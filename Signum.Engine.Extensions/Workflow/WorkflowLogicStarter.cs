﻿using Signum.Engine;
using Signum.Engine.DynamicQuery;
using Signum.Engine.Maps;
using Signum.Engine.Operations;
using Signum.Entities;
using Signum.Entities.Workflow;
using Signum.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Signum.Engine.Workflow
{

    public static class WorkflowLogicStarter
    {
        public static void Start(SchemaBuilder sb, DynamicQueryManager dqm, Func<WorkflowConfigurationEntity> getConfiguration)
        {
            WorkflowLogic.Start(sb, dqm, getConfiguration);
            CaseActivityLogic.Start(sb, dqm);
            WorkflowEventTaskLogic.Start(sb, dqm);
        }
    }
}
