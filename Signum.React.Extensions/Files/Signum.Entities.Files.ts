//////////////////////////////////
//Auto-generated. Do NOT modify!//
//////////////////////////////////

import { MessageKey, QueryKey, Type, EnumType, registerSymbol } from '../../../Framework/Signum.React/Scripts/Reflection'
import * as Entities from '../../../Framework/Signum.React/Scripts/Signum.Entities'
import * as Patterns from '../../../Framework/Signum.React/Scripts/Signum.Entities.Patterns'


export interface IFile
{
    binaryFile?: string | null;
    fileName?: string | null;
    fullWebPath?: string | null; 
}

export interface FileEntity extends IFile { }
export interface EmbeddedFileEntity extends IFile { }

export interface IFilePath extends IFile
{
   fullPhysicalPath?: string | null;
   fileType?: FileTypeSymbol | null; 
   suffix?: string | null;
}

export interface FilePathEntity extends IFilePath { }
export interface EmbeddedFilePathEntity extends IFilePath { }
export const EmbeddedFileEntity = new Type<EmbeddedFileEntity>("EmbeddedFileEntity");
export interface EmbeddedFileEntity extends Entities.EmbeddedEntity {
    Type: "EmbeddedFileEntity";
    fileName?: string | null;
    binaryFile?: string | null;
}

export const EmbeddedFilePathEntity = new Type<EmbeddedFilePathEntity>("EmbeddedFilePathEntity");
export interface EmbeddedFilePathEntity extends Entities.EmbeddedEntity {
    Type: "EmbeddedFilePathEntity";
    fileName?: string | null;
    binaryFile?: string | null;
    fileLength?: number;
    fileLengthString?: string | null;
    suffix?: string | null;
    calculatedDirectory?: string | null;
    fileType?: FileTypeSymbol | null;
}

export const FileEntity = new Type<FileEntity>("File");
export interface FileEntity extends Entities.ImmutableEntity {
    Type: "File";
    fileName?: string | null;
    hash?: string | null;
    binaryFile?: string | null;
}

export module FileMessage {
    export const DownloadFile = new MessageKey("FileMessage", "DownloadFile");
    export const ErrorSavingFile = new MessageKey("FileMessage", "ErrorSavingFile");
    export const FileTypes = new MessageKey("FileMessage", "FileTypes");
    export const Open = new MessageKey("FileMessage", "Open");
    export const OpeningHasNotDefaultImplementationFor0 = new MessageKey("FileMessage", "OpeningHasNotDefaultImplementationFor0");
    export const WebDownload = new MessageKey("FileMessage", "WebDownload");
    export const WebImage = new MessageKey("FileMessage", "WebImage");
    export const Remove = new MessageKey("FileMessage", "Remove");
    export const SavingHasNotDefaultImplementationFor0 = new MessageKey("FileMessage", "SavingHasNotDefaultImplementationFor0");
    export const SelectFile = new MessageKey("FileMessage", "SelectFile");
    export const ViewFile = new MessageKey("FileMessage", "ViewFile");
    export const ViewingHasNotDefaultImplementationFor0 = new MessageKey("FileMessage", "ViewingHasNotDefaultImplementationFor0");
    export const OnlyOneFileIsSupported = new MessageKey("FileMessage", "OnlyOneFileIsSupported");
    export const DragAndDropHere = new MessageKey("FileMessage", "DragAndDropHere");
}

export const FilePathEntity = new Type<FilePathEntity>("FilePath");
export interface FilePathEntity extends Patterns.LockableEntity {
    Type: "FilePath";
    creationDate?: string;
    fileName?: string | null;
    binaryFile?: string | null;
    fileLength?: number;
    fileLengthString?: string | null;
    suffix?: string | null;
    calculatedDirectory?: string | null;
    fileType?: FileTypeSymbol | null;
}

export module FilePathOperation {
    export const Save : Entities.ExecuteSymbol<FilePathEntity> = registerSymbol("Operation", "FilePathOperation.Save");
}

export const FileTypeSymbol = new Type<FileTypeSymbol>("FileType");
export interface FileTypeSymbol extends Entities.Symbol {
    Type: "FileType";
}


