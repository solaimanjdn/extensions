//////////////////////////////////
//Auto-generated. Do NOT modify!//
//////////////////////////////////

import { MessageKey, QueryKey, Type, EnumType, registerSymbol } from '../../../Framework/Signum.React/Scripts/Reflection'
import * as Entities from '../../../Framework/Signum.React/Scripts/Signum.Entities'
import * as Basics from '../../../Framework/Signum.React/Scripts/Signum.Entities.Basics'
import * as Signum from '../Basics/Signum.Entities.Basics'
import * as Files from '../Files/Signum.Entities.Files'
import * as Authorization from '../Authorization/Signum.Entities.Authorization'


export const SystemWordTemplateEntity = new Type<SystemWordTemplateEntity>("SystemWordTemplate");
export interface SystemWordTemplateEntity extends Entities.Entity {
    Type: "SystemWordTemplate";
    fullClassName?: string | null;
}

export const WordConverterSymbol = new Type<WordConverterSymbol>("WordConverter");
export interface WordConverterSymbol extends Entities.Symbol {
    Type: "WordConverter";
}

export const WordTemplateEntity = new Type<WordTemplateEntity>("WordTemplate");
export interface WordTemplateEntity extends Entities.Entity {
    Type: "WordTemplate";
    name?: string | null;
    query?: Basics.QueryEntity | null;
    systemWordTemplate?: SystemWordTemplateEntity | null;
    culture?: Signum.CultureInfoEntity | null;
    active?: boolean;
    startDate?: string | null;
    endDate?: string | null;
    disableAuthorization?: boolean;
    template?: Entities.Lite<Files.FileEntity> | null;
    fileName?: string | null;
    wordTransformer?: WordTransformerSymbol | null;
    wordConverter?: WordConverterSymbol | null;
}

export module WordTemplateMessage {
    export const ModelShouldBeSetToUseModel0 = new MessageKey("WordTemplateMessage", "ModelShouldBeSetToUseModel0");
    export const Type0DoesNotHaveAPropertyWithName1 = new MessageKey("WordTemplateMessage", "Type0DoesNotHaveAPropertyWithName1");
    export const ChooseAReportTemplate = new MessageKey("WordTemplateMessage", "ChooseAReportTemplate");
    export const _01RequiresExtraParameters = new MessageKey("WordTemplateMessage", "_01RequiresExtraParameters");
    export const SelectTheSourceOfDataForYourTableOrChart = new MessageKey("WordTemplateMessage", "SelectTheSourceOfDataForYourTableOrChart");
    export const WriteThisKeyAsTileInTheAlternativeTextOfYourTableOrChart = new MessageKey("WordTemplateMessage", "WriteThisKeyAsTileInTheAlternativeTextOfYourTableOrChart");
}

export module WordTemplateOperation {
    export const Save : Entities.ExecuteSymbol<WordTemplateEntity> = registerSymbol("Operation", "WordTemplateOperation.Save");
    export const Delete : Entities.DeleteSymbol<WordTemplateEntity> = registerSymbol("Operation", "WordTemplateOperation.Delete");
    export const CreateWordReport : Entities.ExecuteSymbol<WordTemplateEntity> = registerSymbol("Operation", "WordTemplateOperation.CreateWordReport");
    export const CreateWordTemplateFromSystemWordTemplate : Entities.ConstructSymbol_From<WordTemplateEntity, SystemWordTemplateEntity> = registerSymbol("Operation", "WordTemplateOperation.CreateWordTemplateFromSystemWordTemplate");
}

export module WordTemplatePermission {
    export const GenerateReport : Authorization.PermissionSymbol = registerSymbol("Permission", "WordTemplatePermission.GenerateReport");
}

export const WordTransformerSymbol = new Type<WordTransformerSymbol>("WordTransformer");
export interface WordTransformerSymbol extends Entities.Symbol {
    Type: "WordTransformer";
}


