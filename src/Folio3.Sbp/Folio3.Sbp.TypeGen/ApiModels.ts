/* tslint:disable */
/* eslint-disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.12.1.0 (NJsonSchema v10.4.6.0 (Newtonsoft.Json v12.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------
// ReSharper disable InconsistentNaming



export interface ResponseDto {
    success?: boolean;
    message?: string | undefined;
    errors?: string[] | undefined;
    traceId?: string | undefined;
}

export interface ResponseDtoOfPagedResponseDtoOfStudentDto extends ResponseDto {
    result?: PagedResponseDtoOfStudentDto | undefined;
}

export interface PagedResponseDtoOfStudentDto {
    data?: StudentDto[] | undefined;
    pageNumber?: number;
    pageSize?: number;
    totalRecords?: number;
}

export interface StudentDto {
    id?: number;
    firstMidName?: string | undefined;
    lastName?: string | undefined;
    enrollmentDate?: Date;
}

export interface ResponseDtoOfStudentDto extends ResponseDto {
    result?: StudentDto | undefined;
}

export interface ResponseDtoOfAuthenticateResponse extends ResponseDto {
    result?: AuthenticateResponse | undefined;
}

export interface AuthenticateResponse {
    token?: string | undefined;
    user?: UserDto | undefined;
}

export interface UserDto {
    id?: string | undefined;
    email?: string | undefined;
    firstName?: string | undefined;
    lastName?: string | undefined;
}

export interface RegisterModel {
    email: string;
    password: string;
    firstName: string;
    lastName: string;
}

export interface AuthenticateRequest {
    userName: string;
    password: string;
}