using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace GlobalToolClsLib
{
	public static class DeepCopyByExpressionTrees
	{
		public class ReferenceEqualityComparer : EqualityComparer<object>
		{
			public override bool Equals(object x, object y)
			{
				return object.ReferenceEquals(x, y);
			}

			public override int GetHashCode(object obj)
			{
				return obj?.GetHashCode() ?? 0;
			}
		}

		private static readonly object IsStructTypeToDeepCopyDictionaryLocker = new object();

		private static Dictionary<Type, bool> IsStructTypeToDeepCopyDictionary = new Dictionary<Type, bool>();

		private static readonly object CompiledCopyFunctionsDictionaryLocker = new object();

		private static Dictionary<Type, Func<object, Dictionary<object, object>, object>> CompiledCopyFunctionsDictionary = new Dictionary<Type, Func<object, Dictionary<object, object>, object>>();

		private static readonly Type ObjectType = typeof(object);

		private static readonly Type ObjectDictionaryType = typeof(Dictionary<object, object>);

		private static readonly Type FieldInfoType = typeof(FieldInfo);

		private static readonly MethodInfo SetValueMethod = FieldInfoType.GetMethod("SetValue", new Type[2] { ObjectType, ObjectType });

		private static readonly Type ThisType = typeof(DeepCopyByExpressionTrees);

		private static readonly MethodInfo DeepCopyByExpressionTreeObjMethod = ThisType.GetMethod("DeepCopyByExpressionTreeObj", BindingFlags.Static | BindingFlags.NonPublic);

		public static T DeepCopy<T>(this T original, Dictionary<object, object> copiedReferencesDict = null)
		{
			return (T)DeepCopyByExpressionTreeObj(original, forceDeepCopy: false, copiedReferencesDict ?? new Dictionary<object, object>(new ReferenceEqualityComparer()));
		}

		private static object DeepCopyByExpressionTreeObj(object original, bool forceDeepCopy, Dictionary<object, object> copiedReferencesDict)
		{
			if (original == null)
			{
				return null;
			}
			Type type = original.GetType();
			if (IsDelegate(type))
			{
				return null;
			}
			if (!forceDeepCopy && !IsTypeToDeepCopy(type))
			{
				return original;
			}
			object alreadyCopiedObject = null;
			if (copiedReferencesDict.TryGetValue(original, out alreadyCopiedObject))
			{
				return alreadyCopiedObject;
			}
			if (type == ObjectType)
			{
				return new object();
			}
			Func<object, Dictionary<object, object>, object> compiledCopyFunction = GetOrCreateCompiledLambdaCopyFunction(type);
			return compiledCopyFunction(original, copiedReferencesDict);
		}

		private static Func<object, Dictionary<object, object>, object> GetOrCreateCompiledLambdaCopyFunction(Type type)
		{
			Func<object, Dictionary<object, object>, object> compiledCopyFunction = null;
			if (!CompiledCopyFunctionsDictionary.TryGetValue(type, out compiledCopyFunction))
			{
				lock (CompiledCopyFunctionsDictionaryLocker)
				{
					if (!CompiledCopyFunctionsDictionary.TryGetValue(type, out compiledCopyFunction))
					{
						Expression<Func<object, Dictionary<object, object>, object>> uncompiledCopyFunction = CreateCompiledLambdaCopyFunctionForType(type);
						compiledCopyFunction = uncompiledCopyFunction.Compile();
						Dictionary<Type, Func<object, Dictionary<object, object>, object>> dictionaryCopy = CompiledCopyFunctionsDictionary.ToDictionary((KeyValuePair<Type, Func<object, Dictionary<object, object>, object>> pair) => pair.Key, (KeyValuePair<Type, Func<object, Dictionary<object, object>, object>> pair) => pair.Value);
						dictionaryCopy.Add(type, compiledCopyFunction);
						CompiledCopyFunctionsDictionary = dictionaryCopy;
					}
				}
			}
			return compiledCopyFunction;
		}

		private static Expression<Func<object, Dictionary<object, object>, object>> CreateCompiledLambdaCopyFunctionForType(Type type)
		{
			InitializeExpressions(type, out var inputParameter, out var inputDictionary, out var outputVariable, out var boxingVariable, out var endLabel, out var variables, out var expressions);
			IfNullThenReturnNullExpression(inputParameter, endLabel, expressions);
			MemberwiseCloneInputToOutputExpression(type, inputParameter, outputVariable, expressions);
			if (IsClassOtherThanString(type))
			{
				StoreReferencesIntoDictionaryExpression(inputParameter, inputDictionary, outputVariable, expressions);
			}
			FieldsCopyExpressions(type, inputParameter, inputDictionary, outputVariable, boxingVariable, expressions);
			if (IsArray(type) && IsTypeToDeepCopy(type.GetElementType()))
			{
				CreateArrayCopyLoopExpression(type, inputParameter, inputDictionary, outputVariable, variables, expressions);
			}
			return CombineAllIntoLambdaFunctionExpression(inputParameter, inputDictionary, outputVariable, endLabel, variables, expressions);
		}

		private static void InitializeExpressions(Type type, out ParameterExpression inputParameter, out ParameterExpression inputDictionary, out ParameterExpression outputVariable, out ParameterExpression boxingVariable, out LabelTarget endLabel, out List<ParameterExpression> variables, out List<Expression> expressions)
		{
			inputParameter = Expression.Parameter(ObjectType);
			inputDictionary = Expression.Parameter(ObjectDictionaryType);
			outputVariable = Expression.Variable(type);
			boxingVariable = Expression.Variable(ObjectType);
			endLabel = Expression.Label();
			variables = new List<ParameterExpression>();
			expressions = new List<Expression>();
			variables.Add(outputVariable);
			variables.Add(boxingVariable);
		}

		private static void IfNullThenReturnNullExpression(ParameterExpression inputParameter, LabelTarget endLabel, List<Expression> expressions)
		{
			ConditionalExpression ifNullThenReturnNullExpression = Expression.IfThen(Expression.Equal(inputParameter, Expression.Constant(null, ObjectType)), Expression.Return(endLabel));
			expressions.Add(ifNullThenReturnNullExpression);
		}

		private static void MemberwiseCloneInputToOutputExpression(Type type, ParameterExpression inputParameter, ParameterExpression outputVariable, List<Expression> expressions)
		{
			MethodInfo memberwiseCloneMethod = ObjectType.GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic);
			BinaryExpression memberwiseCloneInputExpression = Expression.Assign(outputVariable, Expression.Convert(Expression.Call(inputParameter, memberwiseCloneMethod), type));
			expressions.Add(memberwiseCloneInputExpression);
		}

		private static void StoreReferencesIntoDictionaryExpression(ParameterExpression inputParameter, ParameterExpression inputDictionary, ParameterExpression outputVariable, List<Expression> expressions)
		{
			BinaryExpression storeReferencesExpression = Expression.Assign(Expression.Property(inputDictionary, ObjectDictionaryType.GetProperty("Item"), inputParameter), Expression.Convert(outputVariable, ObjectType));
			expressions.Add(storeReferencesExpression);
		}

		private static Expression<Func<object, Dictionary<object, object>, object>> CombineAllIntoLambdaFunctionExpression(ParameterExpression inputParameter, ParameterExpression inputDictionary, ParameterExpression outputVariable, LabelTarget endLabel, List<ParameterExpression> variables, List<Expression> expressions)
		{
			expressions.Add(Expression.Label(endLabel));
			expressions.Add(Expression.Convert(outputVariable, ObjectType));
			BlockExpression finalBody = Expression.Block(variables, expressions);
			return Expression.Lambda<Func<object, Dictionary<object, object>, object>>(finalBody, new ParameterExpression[2] { inputParameter, inputDictionary });
		}

		private static void CreateArrayCopyLoopExpression(Type type, ParameterExpression inputParameter, ParameterExpression inputDictionary, ParameterExpression outputVariable, List<ParameterExpression> variables, List<Expression> expressions)
		{
			int rank = type.GetArrayRank();
			List<ParameterExpression> indices = GenerateIndices(rank);
			variables.AddRange(indices);
			Type elementType = type.GetElementType();
			BinaryExpression assignExpression = ArrayFieldToArrayFieldAssignExpression(inputParameter, inputDictionary, outputVariable, elementType, type, indices);
			Expression forExpression = assignExpression;
			for (int dimension = 0; dimension < rank; dimension++)
			{
				ParameterExpression indexVariable = indices[dimension];
				forExpression = LoopIntoLoopExpression(inputParameter, indexVariable, forExpression, dimension);
			}
			expressions.Add(forExpression);
		}

		private static List<ParameterExpression> GenerateIndices(int arrayRank)
		{
			List<ParameterExpression> indices = new List<ParameterExpression>();
			for (int i = 0; i < arrayRank; i++)
			{
				ParameterExpression indexVariable = Expression.Variable(typeof(int));
				indices.Add(indexVariable);
			}
			return indices;
		}

		private static BinaryExpression ArrayFieldToArrayFieldAssignExpression(ParameterExpression inputParameter, ParameterExpression inputDictionary, ParameterExpression outputVariable, Type elementType, Type arrayType, List<ParameterExpression> indices)
		{
			IndexExpression indexTo = Expression.ArrayAccess(outputVariable, indices);
			MethodCallExpression indexFrom = Expression.ArrayIndex(Expression.Convert(inputParameter, arrayType), indices);
			bool forceDeepCopy = elementType != ObjectType;
			UnaryExpression rightSide = Expression.Convert(Expression.Call(DeepCopyByExpressionTreeObjMethod, Expression.Convert(indexFrom, ObjectType), Expression.Constant(forceDeepCopy, typeof(bool)), inputDictionary), elementType);
			return Expression.Assign(indexTo, rightSide);
		}

		private static BlockExpression LoopIntoLoopExpression(ParameterExpression inputParameter, ParameterExpression indexVariable, Expression loopToEncapsulate, int dimension)
		{
			ParameterExpression lengthVariable = Expression.Variable(typeof(int));
			LabelTarget endLabelForThisLoop = Expression.Label();
			LoopExpression newLoop = Expression.Loop(Expression.Block(new ParameterExpression[0], Expression.IfThen(Expression.GreaterThanOrEqual(indexVariable, lengthVariable), Expression.Break(endLabelForThisLoop)), loopToEncapsulate, Expression.PostIncrementAssign(indexVariable)), endLabelForThisLoop);
			BinaryExpression lengthAssignment = GetLengthForDimensionExpression(lengthVariable, inputParameter, dimension);
			BinaryExpression indexAssignment = Expression.Assign(indexVariable, Expression.Constant(0));
			return Expression.Block(new ParameterExpression[1] { lengthVariable }, lengthAssignment, indexAssignment, newLoop);
		}

		private static BinaryExpression GetLengthForDimensionExpression(ParameterExpression lengthVariable, ParameterExpression inputParameter, int i)
		{
			MethodInfo getLengthMethod = typeof(Array).GetMethod("GetLength", BindingFlags.Instance | BindingFlags.Public);
			ConstantExpression dimensionConstant = Expression.Constant(i);
			return Expression.Assign(lengthVariable, Expression.Call(Expression.Convert(inputParameter, typeof(Array)), getLengthMethod, dimensionConstant));
		}

		private static void FieldsCopyExpressions(Type type, ParameterExpression inputParameter, ParameterExpression inputDictionary, ParameterExpression outputVariable, ParameterExpression boxingVariable, List<Expression> expressions)
		{
			FieldInfo[] fields = GetAllRelevantFields(type);
			List<FieldInfo> readonlyFields = fields.Where((FieldInfo f) => f.IsInitOnly).ToList();
			List<FieldInfo> writableFields = fields.Where((FieldInfo f) => !f.IsInitOnly).ToList();
			bool shouldUseBoxing = readonlyFields.Any();
			if (shouldUseBoxing)
			{
				BinaryExpression boxingExpression = Expression.Assign(boxingVariable, Expression.Convert(outputVariable, ObjectType));
				expressions.Add(boxingExpression);
			}
			foreach (FieldInfo field in readonlyFields)
			{
				if (IsDelegate(field.FieldType))
				{
					ReadonlyFieldToNullExpression(field, boxingVariable, expressions);
				}
				else
				{
					ReadonlyFieldCopyExpression(type, field, inputParameter, inputDictionary, boxingVariable, expressions);
				}
			}
			if (shouldUseBoxing)
			{
				BinaryExpression unboxingExpression = Expression.Assign(outputVariable, Expression.Convert(boxingVariable, type));
				expressions.Add(unboxingExpression);
			}
			foreach (FieldInfo field in writableFields)
			{
				if (IsDelegate(field.FieldType))
				{
					WritableFieldToNullExpression(field, outputVariable, expressions);
				}
				else
				{
					WritableFieldCopyExpression(type, field, inputParameter, inputDictionary, outputVariable, expressions);
				}
			}
		}

		private static FieldInfo[] GetAllRelevantFields(Type type, bool forceAllFields = false)
		{
			List<FieldInfo> fieldsList = new List<FieldInfo>();
			Type typeCache = type;
			while (typeCache != null)
			{
				fieldsList.AddRange(from field in typeCache.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy)
									where forceAllFields || IsTypeToDeepCopy(field.FieldType)
									select field);
				typeCache = typeCache.BaseType;
			}
			return fieldsList.ToArray();
		}

		private static FieldInfo[] GetAllFields(Type type)
		{
			return GetAllRelevantFields(type, forceAllFields: true);
		}

		private static void ReadonlyFieldToNullExpression(FieldInfo field, ParameterExpression boxingVariable, List<Expression> expressions)
		{
			MethodCallExpression fieldToNullExpression = Expression.Call(Expression.Constant(field), SetValueMethod, boxingVariable, Expression.Constant(null, field.FieldType));
			expressions.Add(fieldToNullExpression);
		}

		private static void ReadonlyFieldCopyExpression(Type type, FieldInfo field, ParameterExpression inputParameter, ParameterExpression inputDictionary, ParameterExpression boxingVariable, List<Expression> expressions)
		{
			MemberExpression fieldFrom = Expression.Field(Expression.Convert(inputParameter, type), field);
			bool forceDeepCopy = field.FieldType != ObjectType;
			MethodCallExpression fieldDeepCopyExpression = Expression.Call(Expression.Constant(field, FieldInfoType), SetValueMethod, boxingVariable, Expression.Call(DeepCopyByExpressionTreeObjMethod, Expression.Convert(fieldFrom, ObjectType), Expression.Constant(forceDeepCopy, typeof(bool)), inputDictionary));
			expressions.Add(fieldDeepCopyExpression);
		}

		private static void WritableFieldToNullExpression(FieldInfo field, ParameterExpression outputVariable, List<Expression> expressions)
		{
			MemberExpression fieldTo = Expression.Field(outputVariable, field);
			BinaryExpression fieldToNullExpression = Expression.Assign(fieldTo, Expression.Constant(null, field.FieldType));
			expressions.Add(fieldToNullExpression);
		}

		private static void WritableFieldCopyExpression(Type type, FieldInfo field, ParameterExpression inputParameter, ParameterExpression inputDictionary, ParameterExpression outputVariable, List<Expression> expressions)
		{
			MemberExpression fieldFrom = Expression.Field(Expression.Convert(inputParameter, type), field);
			Type fieldType = field.FieldType;
			MemberExpression fieldTo = Expression.Field(outputVariable, field);
			bool forceDeepCopy = field.FieldType != ObjectType;
			BinaryExpression fieldDeepCopyExpression = Expression.Assign(fieldTo, Expression.Convert(Expression.Call(DeepCopyByExpressionTreeObjMethod, Expression.Convert(fieldFrom, ObjectType), Expression.Constant(forceDeepCopy, typeof(bool)), inputDictionary), fieldType));
			expressions.Add(fieldDeepCopyExpression);
		}

		private static bool IsArray(Type type)
		{
			return type.IsArray;
		}

		private static bool IsDelegate(Type type)
		{
			return typeof(Delegate).IsAssignableFrom(type);
		}

		private static bool IsTypeToDeepCopy(Type type)
		{
			return IsClassOtherThanString(type) || IsStructWhichNeedsDeepCopy(type);
		}

		private static bool IsClassOtherThanString(Type type)
		{
			return !type.IsValueType && type != typeof(string);
		}

		private static bool IsStructWhichNeedsDeepCopy(Type type)
		{
			if (!IsStructTypeToDeepCopyDictionary.TryGetValue(type, out var isStructTypeToDeepCopy))
			{
				lock (IsStructTypeToDeepCopyDictionaryLocker)
				{
					if (!IsStructTypeToDeepCopyDictionary.TryGetValue(type, out isStructTypeToDeepCopy))
					{
						isStructTypeToDeepCopy = IsStructWhichNeedsDeepCopy_NoDictionaryUsed(type);
						Dictionary<Type, bool> newDictionary = IsStructTypeToDeepCopyDictionary.ToDictionary((KeyValuePair<Type, bool> pair) => pair.Key, (KeyValuePair<Type, bool> pair) => pair.Value);
						newDictionary[type] = isStructTypeToDeepCopy;
						IsStructTypeToDeepCopyDictionary = newDictionary;
					}
				}
			}
			return isStructTypeToDeepCopy;
		}

		private static bool IsStructWhichNeedsDeepCopy_NoDictionaryUsed(Type type)
		{
			return IsStructOtherThanBasicValueTypes(type) && HasInItsHierarchyFieldsWithClasses(type);
		}

		private static bool IsStructOtherThanBasicValueTypes(Type type)
		{
			return type.IsValueType && !type.IsPrimitive && !type.IsEnum && type != typeof(decimal);
		}

		private static bool HasInItsHierarchyFieldsWithClasses(Type type, HashSet<Type> alreadyCheckedTypes = null)
		{
			alreadyCheckedTypes = alreadyCheckedTypes ?? new HashSet<Type>();
			alreadyCheckedTypes.Add(type);
			FieldInfo[] allFields = GetAllFields(type);
			List<Type> allFieldTypes = allFields.Select((FieldInfo f) => f.FieldType).Distinct().ToList();
			if (allFieldTypes.Any(IsClassOtherThanString))
			{
				return true;
			}
			List<Type> notBasicStructsTypes = allFieldTypes.Where(IsStructOtherThanBasicValueTypes).ToList();
			List<Type> typesToCheck = notBasicStructsTypes.Where((Type t) => !alreadyCheckedTypes.Contains(t)).ToList();
			foreach (Type typeToCheck in typesToCheck)
			{
				if (HasInItsHierarchyFieldsWithClasses(typeToCheck, alreadyCheckedTypes))
				{
					return true;
				}
			}
			return false;
		}
	}

}
