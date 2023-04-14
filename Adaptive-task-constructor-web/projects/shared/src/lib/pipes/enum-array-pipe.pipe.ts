import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'enumArrayPipe'
})
export class EnumArrayPipePipe implements PipeTransform {
  transform(object: any, ...args: unknown[]) {
    let keys = Object.keys(object);
    return keys.slice(keys.length / 2).map(k => { return  {key: k, value: object[k]}});
  }

}
