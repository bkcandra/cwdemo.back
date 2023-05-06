export interface IServiceResponse<T> {
  valid: boolean;
  message: string[];
  content?: T;
  statusCode: number;
}

export class ServiceResponse<T> implements IServiceResponse<T> {
  valid: boolean;
  message: string[];
  content?: T;
  statusCode: number;

  constructor(success: boolean = true, message: string[] = [], content?: T, statusCode: number = 200) {
    this.valid = success;
    this.message = message;
    this.content = content;
    this.statusCode = statusCode;
  }
}