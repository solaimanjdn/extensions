//////////////////////////////////
//Auto-generated. Do NOT modify!//
//////////////////////////////////

import { MessageKey, QueryKey, Type, EnumType, registerSymbol } from '../../../Framework/Signum.React/Scripts/Reflection'
import * as Entities from '../../../Framework/Signum.React/Scripts/Signum.Entities'
import * as Basics from '../../../Framework/Signum.React/Scripts/Signum.Entities.Basics'
import * as Authorization from '../Authorization/Signum.Entities.Authorization'
import * as Dynamic from '../Dynamic/Signum.Entities.Dynamic'
import * as Signum from '../Basics/Signum.Entities.Basics'
import * as Scheduler from '../Scheduler/Signum.Entities.Scheduler'
import * as Processes from '../Processes/Signum.Entities.Processes'


interface IWorkflowConditionEvaluator {}
interface IWorkflowActionExecutor {}
interface IWorkflowLaneActorsEvaluator {}
interface ISubEntitiesEvaluator{}
interface IWorkflowScriptExecutor{}
interface IWorkflowEventTaskConditionEvaluator{}
interface IWorkflowEventTaskActionEval{}

export interface WorkflowEntitiesDictionary {
    [bpmnElementId: string]: Entities.ModelEntity
}

export const BpmnEntityPair = new Type<BpmnEntityPair>("BpmnEntityPair");
export interface BpmnEntityPair extends Entities.EmbeddedEntity {
    Type: "BpmnEntityPair";
    model: Entities.ModelEntity;
    bpmnElementId: string;
}

export const CaseActivityEntity = new Type<CaseActivityEntity>("CaseActivity");
export interface CaseActivityEntity extends Entities.Entity {
    Type: "CaseActivity";
    case: CaseEntity;
    workflowActivity: WorkflowActivityEntity;
    originalWorkflowActivityName: string;
    startDate: string;
    previous: Entities.Lite<CaseActivityEntity> | null;
    note: string | null;
    doneDate: string | null;
    duration: number | null;
    doneBy: Entities.Lite<Authorization.UserEntity> | null;
    doneType: DoneType | null;
    scriptExecution: ScriptExecutionEntity | null;
}

export module CaseActivityMessage {
    export const CaseContainsOtherActivities = new MessageKey("CaseActivityMessage", "CaseContainsOtherActivities");
    export const NoNextConnectionThatSatisfiesTheConditionsFound = new MessageKey("CaseActivityMessage", "NoNextConnectionThatSatisfiesTheConditionsFound");
    export const CaseIsADecompositionOf0 = new MessageKey("CaseActivityMessage", "CaseIsADecompositionOf0");
    export const From0On1 = new MessageKey("CaseActivityMessage", "From0On1");
    export const DoneBy0On1 = new MessageKey("CaseActivityMessage", "DoneBy0On1");
    export const PersonalRemarksForThisNotification = new MessageKey("CaseActivityMessage", "PersonalRemarksForThisNotification");
    export const TheActivity0RequiresToBeOpened = new MessageKey("CaseActivityMessage", "TheActivity0RequiresToBeOpened");
    export const NoOpenedOrInProgressNotificationsFound = new MessageKey("CaseActivityMessage", "NoOpenedOrInProgressNotificationsFound");
    export const NextActivityAlreadyInProgress = new MessageKey("CaseActivityMessage", "NextActivityAlreadyInProgress");
    export const NextActivityOfDecompositionSurrogateAlreadyInProgress = new MessageKey("CaseActivityMessage", "NextActivityOfDecompositionSurrogateAlreadyInProgress");
    export const Only0CanUndoThisOperation = new MessageKey("CaseActivityMessage", "Only0CanUndoThisOperation");
    export const Activity0HasNoJumps = new MessageKey("CaseActivityMessage", "Activity0HasNoJumps");
    export const Activity0HasNoReject = new MessageKey("CaseActivityMessage", "Activity0HasNoReject");
    export const Activity0HasNoTimeout = new MessageKey("CaseActivityMessage", "Activity0HasNoTimeout");
    export const ThereIsNoPreviousActivity = new MessageKey("CaseActivityMessage", "ThereIsNoPreviousActivity");
    export const OnlyForScriptWorkflowActivities = new MessageKey("CaseActivityMessage", "OnlyForScriptWorkflowActivities");
}

export module CaseActivityOperation {
    export const CreateCaseActivityFromWorkflow : Entities.ConstructSymbol_From<CaseActivityEntity, WorkflowEntity> = registerSymbol("Operation", "CaseActivityOperation.CreateCaseActivityFromWorkflow");
    export const CreateCaseFromWorkflowEventTask : Entities.ConstructSymbol_From<CaseEntity, WorkflowEventTaskEntity> = registerSymbol("Operation", "CaseActivityOperation.CreateCaseFromWorkflowEventTask");
    export const Register : Entities.ExecuteSymbol<CaseActivityEntity> = registerSymbol("Operation", "CaseActivityOperation.Register");
    export const Delete : Entities.DeleteSymbol<CaseActivityEntity> = registerSymbol("Operation", "CaseActivityOperation.Delete");
    export const Next : Entities.ExecuteSymbol<CaseActivityEntity> = registerSymbol("Operation", "CaseActivityOperation.Next");
    export const Approve : Entities.ExecuteSymbol<CaseActivityEntity> = registerSymbol("Operation", "CaseActivityOperation.Approve");
    export const Decline : Entities.ExecuteSymbol<CaseActivityEntity> = registerSymbol("Operation", "CaseActivityOperation.Decline");
    export const Jump : Entities.ExecuteSymbol<CaseActivityEntity> = registerSymbol("Operation", "CaseActivityOperation.Jump");
    export const Reject : Entities.ExecuteSymbol<CaseActivityEntity> = registerSymbol("Operation", "CaseActivityOperation.Reject");
    export const Timeout : Entities.ExecuteSymbol<CaseActivityEntity> = registerSymbol("Operation", "CaseActivityOperation.Timeout");
    export const MarkAsUnread : Entities.ExecuteSymbol<CaseActivityEntity> = registerSymbol("Operation", "CaseActivityOperation.MarkAsUnread");
    export const Undo : Entities.ExecuteSymbol<CaseActivityEntity> = registerSymbol("Operation", "CaseActivityOperation.Undo");
    export const ScriptExecute : Entities.ExecuteSymbol<CaseActivityEntity> = registerSymbol("Operation", "CaseActivityOperation.ScriptExecute");
    export const ScriptScheduleRetry : Entities.ExecuteSymbol<CaseActivityEntity> = registerSymbol("Operation", "CaseActivityOperation.ScriptScheduleRetry");
    export const ScriptFailureJump : Entities.ExecuteSymbol<CaseActivityEntity> = registerSymbol("Operation", "CaseActivityOperation.ScriptFailureJump");
    export const FixCaseDescriptions : Entities.ExecuteSymbol<Dynamic.DynamicTypeEntity> = registerSymbol("Operation", "CaseActivityOperation.FixCaseDescriptions");
}

export module CaseActivityProcessAlgorithm {
    export const Timeout : Processes.ProcessAlgorithmSymbol = registerSymbol("ProcessAlgorithm", "CaseActivityProcessAlgorithm.Timeout");
}

export module CaseActivityQuery {
    export const Inbox = new QueryKey("CaseActivityQuery", "Inbox");
}

export module CaseActivityTask {
    export const Timeout : Scheduler.SimpleTaskSymbol = registerSymbol("SimpleTask", "CaseActivityTask.Timeout");
}

export const CaseEntity = new Type<CaseEntity>("Case");
export interface CaseEntity extends Entities.Entity {
    Type: "Case";
    workflow: WorkflowEntity;
    parentCase: CaseEntity | null;
    description: string;
    mainEntity: ICaseMainEntity;
    startDate: string;
    finishDate: string | null;
}

export const CaseFlowColor = new EnumType<CaseFlowColor>("CaseFlowColor");
export type CaseFlowColor =
    "CaseMaxDuration" |
    "AverageDuration" |
    "EstimatedDuration";

export const CaseJunctionEntity = new Type<CaseJunctionEntity>("CaseJunction");
export interface CaseJunctionEntity extends Entities.Entity {
    Type: "CaseJunction";
    direction?: WorkflowGatewayDirection;
    from?: Entities.Lite<CaseActivityEntity> | null;
    to?: Entities.Lite<CaseActivityEntity> | null;
}

export const CaseNotificationEntity = new Type<CaseNotificationEntity>("CaseNotification");
export interface CaseNotificationEntity extends Entities.Entity {
    Type: "CaseNotification";
    caseActivity?: Entities.Lite<CaseActivityEntity> | null;
    user?: Entities.Lite<Authorization.UserEntity> | null;
    actor?: Entities.Lite<Entities.Entity> | null;
    remarks?: string | null;
    state?: CaseNotificationState;
}

export module CaseNotificationOperation {
    export const SetRemarks : Entities.ExecuteSymbol<CaseNotificationEntity> = registerSymbol("Operation", "CaseNotificationOperation.SetRemarks");
}

export const CaseNotificationState = new EnumType<CaseNotificationState>("CaseNotificationState");
export type CaseNotificationState =
    "New" |
    "Opened" |
    "InProgress" |
    "Done" |
    "DoneByOther";

export module CaseOperation {
    export const SetTags : Entities.ExecuteSymbol<CaseEntity> = registerSymbol("Operation", "CaseOperation.SetTags");
}

export const CaseTagEntity = new Type<CaseTagEntity>("CaseTag");
export interface CaseTagEntity extends Entities.Entity {
    Type: "CaseTag";
    creationDate?: string;
    case?: Entities.Lite<CaseEntity> | null;
    tagType?: CaseTagTypeEntity | null;
    createdBy?: Entities.Lite<Basics.IUserEntity> | null;
}

export const CaseTagsModel = new Type<CaseTagsModel>("CaseTagsModel");
export interface CaseTagsModel extends Entities.ModelEntity {
    Type: "CaseTagsModel";
    caseTags: Entities.MList<CaseTagTypeEntity>;
    oldCaseTags: Entities.MList<CaseTagTypeEntity>;
}

export const CaseTagTypeEntity = new Type<CaseTagTypeEntity>("CaseTagType");
export interface CaseTagTypeEntity extends Entities.Entity {
    Type: "CaseTagType";
    name?: string | null;
    color?: string | null;
}

export module CaseTagTypeOperation {
    export const Save : Entities.ExecuteSymbol<CaseTagTypeEntity> = registerSymbol("Operation", "CaseTagTypeOperation.Save");
}

export const DateFilterRange = new EnumType<DateFilterRange>("DateFilterRange");
export type DateFilterRange =
    "All" |
    "LastWeek" |
    "LastMonth" |
    "CurrentYear";

export const DecisionResult = new EnumType<DecisionResult>("DecisionResult");
export type DecisionResult =
    "Approve" |
    "Decline";

export const DoneType = new EnumType<DoneType>("DoneType");
export type DoneType =
    "Next" |
    "Approve" |
    "Decline" |
    "Jump" |
    "Rejected" |
    "Timeout" |
    "ScriptSuccess" |
    "ScriptFailure";

export interface ICaseMainEntity extends Entities.Entity {
}

export const InboxFilterModel = new Type<InboxFilterModel>("InboxFilterModel");
export interface InboxFilterModel extends Entities.ModelEntity {
    Type: "InboxFilterModel";
    range?: DateFilterRange;
    states: Entities.MList<CaseNotificationState>;
    fromDate?: string | null;
    toDate?: string | null;
}

export module InboxFilterModelMessage {
    export const Clear = new MessageKey("InboxFilterModelMessage", "Clear");
}

export interface IWorkflowNodeEntity extends IWorkflowObjectEntity, Entities.Entity {
    lane?: WorkflowLaneEntity | null;
}

export interface IWorkflowObjectEntity extends Entities.Entity {
    xml?: WorkflowXmlEntity | null;
    name?: string | null;
    bpmnElementId?: string | null;
}

export const ScriptExecutionEntity = new Type<ScriptExecutionEntity>("ScriptExecutionEntity");
export interface ScriptExecutionEntity extends Entities.EmbeddedEntity {
    Type: "ScriptExecutionEntity";
    nextExecution?: string;
    retryCount?: number;
    processIdentifier?: string | null;
}

export const SubEntitiesEval = new Type<SubEntitiesEval>("SubEntitiesEval");
export interface SubEntitiesEval extends Dynamic.EvalEntity<ISubEntitiesEvaluator> {
    Type: "SubEntitiesEval";
}

export const SubWorkflowEntity = new Type<SubWorkflowEntity>("SubWorkflowEntity");
export interface SubWorkflowEntity extends Entities.EmbeddedEntity {
    Type: "SubWorkflowEntity";
    workflow?: WorkflowEntity | null;
    subEntitiesEval?: SubEntitiesEval | null;
}

export const TriggeredOn = new EnumType<TriggeredOn>("TriggeredOn");
export type TriggeredOn =
    "Always" |
    "ConditionIsTrue" |
    "ConditionChangesToTrue";

export const WorkflowActionEntity = new Type<WorkflowActionEntity>("WorkflowAction");
export interface WorkflowActionEntity extends Entities.Entity {
    Type: "WorkflowAction";
    name?: string | null;
    mainEntityType?: Basics.TypeEntity | null;
    eval?: WorkflowActionEval | null;
}

export const WorkflowActionEval = new Type<WorkflowActionEval>("WorkflowActionEval");
export interface WorkflowActionEval extends Dynamic.EvalEntity<IWorkflowActionExecutor> {
    Type: "WorkflowActionEval";
}

export module WorkflowActionOperation {
    export const Save : Entities.ExecuteSymbol<WorkflowActionEntity> = registerSymbol("Operation", "WorkflowActionOperation.Save");
    export const Delete : Entities.DeleteSymbol<WorkflowActionEntity> = registerSymbol("Operation", "WorkflowActionOperation.Delete");
}

export const WorkflowActivityEntity = new Type<WorkflowActivityEntity>("WorkflowActivity");
export interface WorkflowActivityEntity extends Entities.Entity, IWorkflowNodeEntity, IWorkflowObjectEntity {
    Type: "WorkflowActivity";
    lane?: WorkflowLaneEntity | null;
    name?: string | null;
    bpmnElementId?: string | null;
    comments?: string | null;
    type?: WorkflowActivityType;
    requiresOpen?: boolean;
    reject?: WorkflowRejectEntity | null;
    timeout?: WorkflowTimeoutEntity | null;
    estimatedDuration?: number | null;
    viewName?: string | null;
    validationRules: Entities.MList<WorkflowActivityValidationEntity>;
    jumps: Entities.MList<WorkflowJumpEntity>;
    script?: WorkflowScriptPartEntity | null;
    xml?: WorkflowXmlEntity | null;
    subWorkflow?: SubWorkflowEntity | null;
    userHelp?: string | null;
}

export module WorkflowActivityMessage {
    export const DuplicateViewNameFound0 = new MessageKey("WorkflowActivityMessage", "DuplicateViewNameFound0");
    export const ChooseADestinationForWorkflowJumping = new MessageKey("WorkflowActivityMessage", "ChooseADestinationForWorkflowJumping");
    export const CaseFlow = new MessageKey("WorkflowActivityMessage", "CaseFlow");
}

export const WorkflowActivityModel = new Type<WorkflowActivityModel>("WorkflowActivityModel");
export interface WorkflowActivityModel extends Entities.ModelEntity {
    Type: "WorkflowActivityModel";
    workflowActivity?: Entities.Lite<WorkflowActivityEntity> | null;
    workflow?: WorkflowEntity | null;
    mainEntityType: Basics.TypeEntity;
    name?: string | null;
    type?: WorkflowActivityType;
    requiresOpen?: boolean;
    reject?: WorkflowRejectEntity | null;
    timeout?: WorkflowTimeoutEntity | null;
    estimatedDuration?: number | null;
    validationRules: Entities.MList<WorkflowActivityValidationEntity>;
    jumps: Entities.MList<WorkflowJumpEntity>;
    script?: WorkflowScriptPartEntity | null;
    viewName?: string | null;
    comments?: string | null;
    userHelp?: string | null;
    subWorkflow?: SubWorkflowEntity | null;
}

export module WorkflowActivityOperation {
    export const Save : Entities.ExecuteSymbol<WorkflowActivityEntity> = registerSymbol("Operation", "WorkflowActivityOperation.Save");
    export const Delete : Entities.DeleteSymbol<WorkflowActivityEntity> = registerSymbol("Operation", "WorkflowActivityOperation.Delete");
}

export const WorkflowActivityType = new EnumType<WorkflowActivityType>("WorkflowActivityType");
export type WorkflowActivityType =
    "Task" |
    "Decision" |
    "DecompositionWorkflow" |
    "CallWorkflow" |
    "Script";

export const WorkflowActivityValidationEntity = new Type<WorkflowActivityValidationEntity>("WorkflowActivityValidationEntity");
export interface WorkflowActivityValidationEntity extends Entities.EmbeddedEntity {
    Type: "WorkflowActivityValidationEntity";
    rule?: Entities.Lite<Dynamic.DynamicValidationEntity> | null;
    onAccept?: boolean;
    onDecline?: boolean;
}

export const WorkflowConditionEntity = new Type<WorkflowConditionEntity>("WorkflowCondition");
export interface WorkflowConditionEntity extends Entities.Entity {
    Type: "WorkflowCondition";
    name?: string | null;
    mainEntityType?: Basics.TypeEntity | null;
    eval?: WorkflowConditionEval | null;
}

export const WorkflowConditionEval = new Type<WorkflowConditionEval>("WorkflowConditionEval");
export interface WorkflowConditionEval extends Dynamic.EvalEntity<IWorkflowConditionEvaluator> {
    Type: "WorkflowConditionEval";
}

export module WorkflowConditionOperation {
    export const Save : Entities.ExecuteSymbol<WorkflowConditionEntity> = registerSymbol("Operation", "WorkflowConditionOperation.Save");
    export const Delete : Entities.DeleteSymbol<WorkflowConditionEntity> = registerSymbol("Operation", "WorkflowConditionOperation.Delete");
}

export const WorkflowConfigurationEntity = new Type<WorkflowConfigurationEntity>("WorkflowConfigurationEntity");
export interface WorkflowConfigurationEntity extends Entities.EmbeddedEntity {
    Type: "WorkflowConfigurationEntity";
    scriptRunnerPeriod?: number;
    avoidExecutingScriptsOlderThan?: number | null;
    chunkSizeRunningScripts?: number;
}

export const WorkflowConnectionEntity = new Type<WorkflowConnectionEntity>("WorkflowConnection");
export interface WorkflowConnectionEntity extends Entities.Entity, IWorkflowObjectEntity {
    Type: "WorkflowConnection";
    from?: IWorkflowNodeEntity | null;
    to?: IWorkflowNodeEntity | null;
    name?: string | null;
    bpmnElementId?: string | null;
    decisonResult?: DecisionResult | null;
    condition?: Entities.Lite<WorkflowConditionEntity> | null;
    action?: Entities.Lite<WorkflowActionEntity> | null;
    order?: number | null;
    xml?: WorkflowXmlEntity | null;
}

export const WorkflowConnectionModel = new Type<WorkflowConnectionModel>("WorkflowConnectionModel");
export interface WorkflowConnectionModel extends Entities.ModelEntity {
    Type: "WorkflowConnectionModel";
    mainEntityType: Basics.TypeEntity;
    name?: string | null;
    needDecisonResult?: boolean;
    needCondition?: boolean;
    needOrder?: boolean;
    decisonResult?: DecisionResult | null;
    condition?: Entities.Lite<WorkflowConditionEntity> | null;
    action?: Entities.Lite<WorkflowActionEntity> | null;
    order?: number | null;
}

export module WorkflowConnectionOperation {
    export const Save : Entities.ExecuteSymbol<WorkflowConnectionEntity> = registerSymbol("Operation", "WorkflowConnectionOperation.Save");
    export const Delete : Entities.DeleteSymbol<WorkflowConnectionEntity> = registerSymbol("Operation", "WorkflowConnectionOperation.Delete");
}

export const WorkflowEntity = new Type<WorkflowEntity>("Workflow");
export interface WorkflowEntity extends Entities.Entity {
    Type: "Workflow";
    name?: string | null;
    mainEntityType?: Basics.TypeEntity | null;
}

export const WorkflowEventEntity = new Type<WorkflowEventEntity>("WorkflowEvent");
export interface WorkflowEventEntity extends Entities.Entity, IWorkflowNodeEntity, IWorkflowObjectEntity {
    Type: "WorkflowEvent";
    name?: string | null;
    bpmnElementId?: string | null;
    lane?: WorkflowLaneEntity | null;
    type?: WorkflowEventType;
    xml?: WorkflowXmlEntity | null;
}

export const WorkflowEventModel = new Type<WorkflowEventModel>("WorkflowEventModel");
export interface WorkflowEventModel extends Entities.ModelEntity {
    Type: "WorkflowEventModel";
    mainEntityType: Basics.TypeEntity;
    name?: string | null;
    type?: WorkflowEventType;
    task?: WorkflowEventTaskModel | null;
}

export module WorkflowEventOperation {
    export const Save : Entities.ExecuteSymbol<WorkflowEventEntity> = registerSymbol("Operation", "WorkflowEventOperation.Save");
    export const Delete : Entities.DeleteSymbol<WorkflowEventEntity> = registerSymbol("Operation", "WorkflowEventOperation.Delete");
}

export const WorkflowEventTaskActionEval = new Type<WorkflowEventTaskActionEval>("WorkflowEventTaskActionEval");
export interface WorkflowEventTaskActionEval extends Dynamic.EvalEntity<IWorkflowEventTaskActionEval> {
    Type: "WorkflowEventTaskActionEval";
}

export const WorkflowEventTaskConditionEval = new Type<WorkflowEventTaskConditionEval>("WorkflowEventTaskConditionEval");
export interface WorkflowEventTaskConditionEval extends Dynamic.EvalEntity<IWorkflowEventTaskConditionEvaluator> {
    Type: "WorkflowEventTaskConditionEval";
}

export const WorkflowEventTaskConditionResultEntity = new Type<WorkflowEventTaskConditionResultEntity>("WorkflowEventTaskConditionResult");
export interface WorkflowEventTaskConditionResultEntity extends Entities.Entity {
    Type: "WorkflowEventTaskConditionResult";
    creationDate?: string;
    workflowEventTask?: Entities.Lite<WorkflowEventTaskEntity> | null;
    result?: boolean;
}

export const WorkflowEventTaskEntity = new Type<WorkflowEventTaskEntity>("WorkflowEventTask");
export interface WorkflowEventTaskEntity extends Entities.Entity, Scheduler.ITaskEntity {
    Type: "WorkflowEventTask";
    workflow?: Entities.Lite<WorkflowEntity> | null;
    event?: Entities.Lite<WorkflowEventEntity> | null;
    triggeredOn?: TriggeredOn;
    condition?: WorkflowEventTaskConditionEval | null;
    action?: WorkflowEventTaskActionEval | null;
}

export const WorkflowEventTaskModel = new Type<WorkflowEventTaskModel>("WorkflowEventTaskModel");
export interface WorkflowEventTaskModel extends Entities.ModelEntity {
    Type: "WorkflowEventTaskModel";
    suspended?: boolean;
    rule?: Scheduler.IScheduleRuleEntity | null;
    triggeredOn?: TriggeredOn;
    condition?: WorkflowEventTaskConditionEval | null;
    action?: WorkflowEventTaskActionEval | null;
}

export module WorkflowEventTaskOperation {
    export const Save : Entities.ExecuteSymbol<WorkflowEventTaskEntity> = registerSymbol("Operation", "WorkflowEventTaskOperation.Save");
    export const Delete : Entities.DeleteSymbol<WorkflowEventTaskEntity> = registerSymbol("Operation", "WorkflowEventTaskOperation.Delete");
}

export const WorkflowEventType = new EnumType<WorkflowEventType>("WorkflowEventType");
export type WorkflowEventType =
    "Start" |
    "TimerStart" |
    "ConditionalStart" |
    "Finish";

export const WorkflowGatewayDirection = new EnumType<WorkflowGatewayDirection>("WorkflowGatewayDirection");
export type WorkflowGatewayDirection =
    "Split" |
    "Join";

export const WorkflowGatewayEntity = new Type<WorkflowGatewayEntity>("WorkflowGateway");
export interface WorkflowGatewayEntity extends Entities.Entity, IWorkflowNodeEntity, IWorkflowObjectEntity {
    Type: "WorkflowGateway";
    lane?: WorkflowLaneEntity | null;
    name?: string | null;
    bpmnElementId?: string | null;
    type?: WorkflowGatewayType;
    direction?: WorkflowGatewayDirection;
    xml?: WorkflowXmlEntity | null;
}

export const WorkflowGatewayModel = new Type<WorkflowGatewayModel>("WorkflowGatewayModel");
export interface WorkflowGatewayModel extends Entities.ModelEntity {
    Type: "WorkflowGatewayModel";
    name?: string | null;
    type?: WorkflowGatewayType;
    direction?: WorkflowGatewayDirection;
}

export module WorkflowGatewayOperation {
    export const Save : Entities.ExecuteSymbol<WorkflowGatewayEntity> = registerSymbol("Operation", "WorkflowGatewayOperation.Save");
    export const Delete : Entities.DeleteSymbol<WorkflowGatewayEntity> = registerSymbol("Operation", "WorkflowGatewayOperation.Delete");
}

export const WorkflowGatewayType = new EnumType<WorkflowGatewayType>("WorkflowGatewayType");
export type WorkflowGatewayType =
    "Exclusive" |
    "Inclusive" |
    "Parallel";

export const WorkflowJumpEntity = new Type<WorkflowJumpEntity>("WorkflowJumpEntity");
export interface WorkflowJumpEntity extends Entities.EmbeddedEntity {
    Type: "WorkflowJumpEntity";
    to?: Entities.Lite<IWorkflowNodeEntity> | null;
    condition?: Entities.Lite<WorkflowConditionEntity> | null;
    action?: Entities.Lite<WorkflowActionEntity> | null;
}

export const WorkflowLaneActorsEval = new Type<WorkflowLaneActorsEval>("WorkflowLaneActorsEval");
export interface WorkflowLaneActorsEval extends Dynamic.EvalEntity<IWorkflowLaneActorsEvaluator> {
    Type: "WorkflowLaneActorsEval";
}

export const WorkflowLaneEntity = new Type<WorkflowLaneEntity>("WorkflowLane");
export interface WorkflowLaneEntity extends Entities.Entity, IWorkflowObjectEntity {
    Type: "WorkflowLane";
    name?: string | null;
    bpmnElementId?: string | null;
    xml?: WorkflowXmlEntity | null;
    pool?: WorkflowPoolEntity | null;
    actors: Entities.MList<Entities.Lite<Entities.Entity>>;
    actorsEval?: WorkflowLaneActorsEval | null;
}

export const WorkflowLaneModel = new Type<WorkflowLaneModel>("WorkflowLaneModel");
export interface WorkflowLaneModel extends Entities.ModelEntity {
    Type: "WorkflowLaneModel";
    mainEntityType: Basics.TypeEntity;
    name?: string | null;
    actors: Entities.MList<Entities.Lite<Entities.Entity>>;
    actorsEval?: WorkflowLaneActorsEval | null;
}

export module WorkflowLaneOperation {
    export const Save : Entities.ExecuteSymbol<WorkflowLaneEntity> = registerSymbol("Operation", "WorkflowLaneOperation.Save");
    export const Delete : Entities.DeleteSymbol<WorkflowLaneEntity> = registerSymbol("Operation", "WorkflowLaneOperation.Delete");
}

export module WorkflowMessage {
    export const _0BelongsToADifferentWorkflow = new MessageKey("WorkflowMessage", "_0BelongsToADifferentWorkflow");
    export const Condition0IsDefinedFor1Not2 = new MessageKey("WorkflowMessage", "Condition0IsDefinedFor1Not2");
    export const JumpsToSameActivityNotAllowed = new MessageKey("WorkflowMessage", "JumpsToSameActivityNotAllowed");
    export const JumpTo0FailedBecause1 = new MessageKey("WorkflowMessage", "JumpTo0FailedBecause1");
    export const ToUse0YouSouldSaveWorkflow = new MessageKey("WorkflowMessage", "ToUse0YouSouldSaveWorkflow");
    export const ToUseNewNodesOnJumpsYouSouldSaveWorkflow = new MessageKey("WorkflowMessage", "ToUseNewNodesOnJumpsYouSouldSaveWorkflow");
    export const ToUse0YouSouldSetTheWorkflow1 = new MessageKey("WorkflowMessage", "ToUse0YouSouldSetTheWorkflow1");
    export const ChangeWorkflowMainEntityTypeIsNotAllowedBecausueWeHaveNodesThatUseIt = new MessageKey("WorkflowMessage", "ChangeWorkflowMainEntityTypeIsNotAllowedBecausueWeHaveNodesThatUseIt");
}

export const WorkflowModel = new Type<WorkflowModel>("WorkflowModel");
export interface WorkflowModel extends Entities.ModelEntity {
    Type: "WorkflowModel";
    diagramXml: string;
    entities: Entities.MList<BpmnEntityPair>;
}

export module WorkflowOperation {
    export const Clone : Entities.ConstructSymbol_From<WorkflowEntity, WorkflowEntity> = registerSymbol("Operation", "WorkflowOperation.Clone");
    export const Save : Entities.ExecuteSymbol<WorkflowEntity> = registerSymbol("Operation", "WorkflowOperation.Save");
    export const Delete : Entities.DeleteSymbol<WorkflowEntity> = registerSymbol("Operation", "WorkflowOperation.Delete");
}

export const WorkflowPoolEntity = new Type<WorkflowPoolEntity>("WorkflowPool");
export interface WorkflowPoolEntity extends Entities.Entity, IWorkflowObjectEntity {
    Type: "WorkflowPool";
    workflow?: WorkflowEntity | null;
    name?: string | null;
    bpmnElementId?: string | null;
    xml?: WorkflowXmlEntity | null;
}

export const WorkflowPoolModel = new Type<WorkflowPoolModel>("WorkflowPoolModel");
export interface WorkflowPoolModel extends Entities.ModelEntity {
    Type: "WorkflowPoolModel";
    name?: string | null;
}

export module WorkflowPoolOperation {
    export const Save : Entities.ExecuteSymbol<WorkflowPoolEntity> = registerSymbol("Operation", "WorkflowPoolOperation.Save");
    export const Delete : Entities.DeleteSymbol<WorkflowPoolEntity> = registerSymbol("Operation", "WorkflowPoolOperation.Delete");
}

export const WorkflowRejectEntity = new Type<WorkflowRejectEntity>("WorkflowRejectEntity");
export interface WorkflowRejectEntity extends Entities.EmbeddedEntity {
    Type: "WorkflowRejectEntity";
    condition?: Entities.Lite<WorkflowConditionEntity> | null;
    action?: Entities.Lite<WorkflowActionEntity> | null;
}

export const WorkflowReplacementItemEntity = new Type<WorkflowReplacementItemEntity>("WorkflowReplacementItemEntity");
export interface WorkflowReplacementItemEntity extends Entities.EmbeddedEntity {
    Type: "WorkflowReplacementItemEntity";
    oldTask: Entities.Lite<WorkflowActivityEntity>;
    newTask?: string | null;
}

export const WorkflowReplacementModel = new Type<WorkflowReplacementModel>("WorkflowReplacementModel");
export interface WorkflowReplacementModel extends Entities.ModelEntity {
    Type: "WorkflowReplacementModel";
    replacements: Entities.MList<WorkflowReplacementItemEntity>;
}

export const WorkflowScriptEntity = new Type<WorkflowScriptEntity>("WorkflowScript");
export interface WorkflowScriptEntity extends Entities.Entity {
    Type: "WorkflowScript";
    name?: string | null;
    mainEntityType?: Basics.TypeEntity | null;
    eval?: WorkflowScriptEval | null;
}

export const WorkflowScriptEval = new Type<WorkflowScriptEval>("WorkflowScriptEval");
export interface WorkflowScriptEval extends Dynamic.EvalEntity<IWorkflowScriptExecutor> {
    Type: "WorkflowScriptEval";
    customTypes?: string | null;
}

export module WorkflowScriptOperation {
    export const Save : Entities.ExecuteSymbol<WorkflowScriptEntity> = registerSymbol("Operation", "WorkflowScriptOperation.Save");
    export const Delete : Entities.DeleteSymbol<WorkflowScriptEntity> = registerSymbol("Operation", "WorkflowScriptOperation.Delete");
}

export const WorkflowScriptPartEntity = new Type<WorkflowScriptPartEntity>("WorkflowScriptPartEntity");
export interface WorkflowScriptPartEntity extends Entities.EmbeddedEntity {
    Type: "WorkflowScriptPartEntity";
    script?: Entities.Lite<WorkflowScriptEntity> | null;
    retryStrategy?: WorkflowScriptRetryStrategyEntity | null;
    onFailureJump?: Entities.Lite<IWorkflowNodeEntity> | null;
}

export const WorkflowScriptRetryStrategyEntity = new Type<WorkflowScriptRetryStrategyEntity>("WorkflowScriptRetryStrategy");
export interface WorkflowScriptRetryStrategyEntity extends Entities.Entity {
    Type: "WorkflowScriptRetryStrategy";
    rule?: string | null;
}

export module WorkflowScriptRetryStrategyOperation {
    export const Save : Entities.ExecuteSymbol<WorkflowScriptRetryStrategyEntity> = registerSymbol("Operation", "WorkflowScriptRetryStrategyOperation.Save");
    export const Delete : Entities.DeleteSymbol<WorkflowScriptRetryStrategyEntity> = registerSymbol("Operation", "WorkflowScriptRetryStrategyOperation.Delete");
}

export module WorkflowScriptRunnerPanelPermission {
    export const ViewWorkflowScriptRunnerPanel : Authorization.PermissionSymbol = registerSymbol("Permission", "WorkflowScriptRunnerPanelPermission.ViewWorkflowScriptRunnerPanel");
}

export const WorkflowTimeoutEntity = new Type<WorkflowTimeoutEntity>("WorkflowTimeoutEntity");
export interface WorkflowTimeoutEntity extends Entities.EmbeddedEntity {
    Type: "WorkflowTimeoutEntity";
    timeout?: Signum.TimeSpanEntity | null;
    to?: Entities.Lite<IWorkflowNodeEntity> | null;
    action?: Entities.Lite<WorkflowActionEntity> | null;
}

export module WorkflowValidationMessage {
    export const NodeType0WithId1IsInvalid = new MessageKey("WorkflowValidationMessage", "NodeType0WithId1IsInvalid");
    export const ParticipantsAndProcessesAreNotSynchronized = new MessageKey("WorkflowValidationMessage", "ParticipantsAndProcessesAreNotSynchronized");
    export const MultipleStartEventsAreNotAllowed = new MessageKey("WorkflowValidationMessage", "MultipleStartEventsAreNotAllowed");
    export const SomeStartEventIsRequired = new MessageKey("WorkflowValidationMessage", "SomeStartEventIsRequired");
    export const TheFollowingTasksAreGoingToBeDeleted = new MessageKey("WorkflowValidationMessage", "TheFollowingTasksAreGoingToBeDeleted");
    export const FinishEventIsRequired = new MessageKey("WorkflowValidationMessage", "FinishEventIsRequired");
    export const Activity0CanNotRejectToStart = new MessageKey("WorkflowValidationMessage", "Activity0CanNotRejectToStart");
    export const _0HasInputs = new MessageKey("WorkflowValidationMessage", "_0HasInputs");
    export const _0HasOutputs = new MessageKey("WorkflowValidationMessage", "_0HasOutputs");
    export const _0HasNoInputs = new MessageKey("WorkflowValidationMessage", "_0HasNoInputs");
    export const _0HasNoOutputs = new MessageKey("WorkflowValidationMessage", "_0HasNoOutputs");
    export const _0HasJustOneInputAndOneOutput = new MessageKey("WorkflowValidationMessage", "_0HasJustOneInputAndOneOutput");
    export const _0HasMultipleOutputs = new MessageKey("WorkflowValidationMessage", "_0HasMultipleOutputs");
    export const Activity0CanNotRejectToParallelGateway = new MessageKey("WorkflowValidationMessage", "Activity0CanNotRejectToParallelGateway");
    export const IsNotInWorkflow = new MessageKey("WorkflowValidationMessage", "IsNotInWorkflow");
    export const Activity0CanNotJumpTo1Because2 = new MessageKey("WorkflowValidationMessage", "Activity0CanNotJumpTo1Because2");
    export const Activity0CanNotTimeoutTo1Because2 = new MessageKey("WorkflowValidationMessage", "Activity0CanNotTimeoutTo1Because2");
    export const IsStart = new MessageKey("WorkflowValidationMessage", "IsStart");
    export const IsSelfJumping = new MessageKey("WorkflowValidationMessage", "IsSelfJumping");
    export const IsInDifferentParallelTrack = new MessageKey("WorkflowValidationMessage", "IsInDifferentParallelTrack");
    export const _0Track1CanNotBeConnectedTo2Track3InsteadOfTrack4 = new MessageKey("WorkflowValidationMessage", "_0Track1CanNotBeConnectedTo2Track3InsteadOfTrack4");
    export const StartEventNextNodeShouldBeAnActivity = new MessageKey("WorkflowValidationMessage", "StartEventNextNodeShouldBeAnActivity");
    export const ParallelGatewaysShouldPair = new MessageKey("WorkflowValidationMessage", "ParallelGatewaysShouldPair");
    export const TimerOrConditionalStartEventsCanNotGoToJoinGateways = new MessageKey("WorkflowValidationMessage", "TimerOrConditionalStartEventsCanNotGoToJoinGateways");
    export const Gateway0ShouldHasConditionOnEachOutput = new MessageKey("WorkflowValidationMessage", "Gateway0ShouldHasConditionOnEachOutput");
    export const Gateway0ShouldHasConditionOnEachOutputExceptTheLast = new MessageKey("WorkflowValidationMessage", "Gateway0ShouldHasConditionOnEachOutputExceptTheLast");
    export const _0CanNotBeConnectodToAParallelJoinBecauseHasNoPreviousParallelSplit = new MessageKey("WorkflowValidationMessage", "_0CanNotBeConnectodToAParallelJoinBecauseHasNoPreviousParallelSplit");
}

export const WorkflowXmlEntity = new Type<WorkflowXmlEntity>("WorkflowXmlEntity");
export interface WorkflowXmlEntity extends Entities.EmbeddedEntity {
    Type: "WorkflowXmlEntity";
    diagramXml?: string | null;
}


