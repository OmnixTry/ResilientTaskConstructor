import { Option } from "../../test/entity/option";

export class Answer {
    id: number;
    taskOptionId: number | null;
    option: Option;
    taskDtoId: number;
    value: string | null;
    corect: boolean;
}