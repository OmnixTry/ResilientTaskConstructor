export class ValidationMessages {
    required = 'This field is required';
    letterMessage = 'This field can only contain letters';
    email = 'Value doesn\'t correspond to email format';
    minLength = 'Value is too short';
    maxLength = 'Value is too long';
    includes = 'This name is not unique';
    notNumeric = 'Value must be numeric';
}

export class SmartMessages {
    min = (error: { min: number }) => `Value can't be less than ${error.min}`;
    max = (error: { max: number }) => `Value can't be more than ${error.max}`;
}