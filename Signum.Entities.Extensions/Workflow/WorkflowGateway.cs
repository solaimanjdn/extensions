﻿using Signum.Entities;
using Signum.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Signum.Entities.Workflow
{
    [Serializable, EntityKind(EntityKind.String, EntityData.Master)]
    public class WorkflowGatewayEntity : Entity, IWorkflowNodeEntity, IWithModel
    {
        [NotNullable]
        [NotNullValidator]
        public WorkflowLaneEntity Lane{ get; set; }
        
        [SqlDbType(Size = 100)]
        [StringLengthValidator(AllowNulls = true, Min = 3, Max = 100)]
        public string Name { get; set; }

        [NotNullable, SqlDbType(Size = 100)]
        [StringLengthValidator(AllowNulls = false, Min = 1, Max = 100)]
        public string BpmnElementId { get; set; }

        public WorkflowGatewayType Type { get; set; }
        public WorkflowGatewayDirection Direction { get; set; }

        [NotNullable]
        [NotNullValidator]
        public WorkflowXmlEntity Xml { get; set; }

        static Expression<Func<WorkflowGatewayEntity, string>> ToStringExpression = @this => @this.Name ?? @this.BpmnElementId;
        [ExpressionField]
        public override string ToString()
        {
            return ToStringExpression.Evaluate(this);
        }
        public ModelEntity GetModel()
        {
            var model = new WorkflowGatewayModel();
            model.Name = this.Name;
            model.Type = this.Type;
            model.Direction = this.Direction;
            return model;
        }

        public void SetModel(ModelEntity model)
        {
            var wModel = (WorkflowGatewayModel)model;
            this.Name = wModel.Name;
            this.Type = wModel.Type;
            this.Direction = wModel.Direction;
        }
    }

    public enum WorkflowGatewayType
    {
       Exclusive, // 1
       Inclusive, // 1...N
       Parallel   // N
    }

    public enum WorkflowGatewayDirection
    {
        Split,
        Join,
    }

    [AutoInit]
    public static class WorkflowGatewayOperation
    {
        public static readonly ExecuteSymbol<WorkflowGatewayEntity> Save;
        public static readonly DeleteSymbol<WorkflowGatewayEntity> Delete;
    }

    [Serializable]
    public class WorkflowGatewayModel : ModelEntity
    {
        [NotNullable, SqlDbType(Size = 100)]
        [StringLengthValidator(AllowNulls = false, Min = 3, Max = 100)]
        public string Name { get; set; }

        public WorkflowGatewayType Type { get; set; }
        public WorkflowGatewayDirection Direction { get; set; }
    }

}
