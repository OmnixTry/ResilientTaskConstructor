import { GroupTest } from "./groupTest";
import { GroupUser } from "./groupUser";

export interface Group {
    id: number,
	name: string,
	teacherId: string,
	teacher: GroupUser,
	tests: GroupTest[],
	students: GroupUser[],
}