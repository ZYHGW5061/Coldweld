using GlobalDataDefineClsLib;
using ModelConfigClsLib.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConfigClsLib
{

    public enum EnumProductRunType
    {
        [Description("自动运行")]
        Auto,

        [Description("单步运行")]
        Step
    }

    public enum EnumProductRunStat
    {
        [Description("未运行")]
        Stop,

        [Description("自动运行中")]
        AutoRun,

        [Description("自动运行暂停")]
        AutoPause,

        [Description("单步运行中")]
        StepRun,

        [Description("完成单步暂停")]
        StepPause,

        [Description("运行结束")]
        Finish
    }

    public interface IStepAction
    {
        GWResult run();
    }

    public class ProductExecutor
    {
        //主配方
        public ProductConfig ProductCfg { get; set; }
        public List<ProductStep> ProductSteps { get; set; }
        public List<IStepAction> StepActions { get; set; }

        //运行状态
        public EnumProductRunStat RunStat { get; set; }

        public ProductExecutor(ProductConfig productCfg)
        {
            ProductCfg = productCfg;
            ProductSteps = productCfg.ProductSteps;
            RunStat = EnumProductRunStat.Stop;
            StepActions = new List<IStepAction>();
            fillStepActions();
        }

        private void fillStepActions()
        {
            foreach(ProductStep step in ProductSteps)
            {
                if (step.productStepType == EnumProductStepType.Translate)
                {
                    fillComStep(step);
                }else if (step.productStepType == EnumProductStepType.Eutectic)
                {
                    fillEutecticStep(step);
                }
            }
            
        }

        private void fillComStep(ProductStep step)
        {
            TestStepAction act1 = new TestStepAction(step, 1, "TestAct1");
            StepActions.Add(act1);
            TestStepAction act2 = new TestStepAction(step, 2, "TestAct2");
            StepActions.Add(act2);
            TestStepAction act3 = new TestStepAction(step, 3, "TestAct3");
            StepActions.Add(act3);
        }

        private void fillEutecticStep(ProductStep step)
        {
            TestStepAction act4 = new TestStepAction(step, 4, "TestAct4");
            StepActions.Add(act4);
            TestStepAction act5 = new TestStepAction(step, 5, "TestAct5");
            StepActions.Add(act5);
            TestStepAction act6 = new TestStepAction(step, 6, "TestAct6");
            StepActions.Add(act6);
        }

        public void Execute()
        {
            foreach(IStepAction act in StepActions)
            {
                act.run();
            }
        }


    }

    //====================================================================== 详细动作定义 =====================================================================
    class TestStepAction : IStepAction
    {
        //动作所属Step
        public ProductStep SrcProductStep { get; set; }

        //动作序号
        public int StepNo { get; set; }

        //动作描述
        public string StepDesc { get; set; }

        public TestStepAction(ProductStep step, int stepNo, string stepDesc)
        {
            SrcProductStep = step;
            StepNo = stepNo;
            StepDesc = stepDesc;
        }

        //动作执行实现
        public GWResult run()
        {
            Console.WriteLine("[<" + SrcProductStep.StepName + ">-" + StepNo + ":" + StepDesc + "]执行---");
            return GlobalGWResultDefine.RET_SUCCESS;
        }
    }

}
