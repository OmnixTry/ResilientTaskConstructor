import { Roles } from "./roles.enum";

export class RegistrationUser {
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    confirmPassword: string;
    role: Roles;
}