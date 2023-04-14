import { GroupUser } from "../../group/entity/groupUser";
import { Attempt } from "./attempt";

export interface StudentResult {
    student: GroupUser;
    attempt: Attempt;
}