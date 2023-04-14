import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'ui-selective-list',
  templateUrl: './selective-list.component.html',
  styleUrls: ['./selective-list.component.scss']
})
export class SelectiveListComponent implements OnInit {
  selectedIndex: number = null;
  _list: any[] = [];

  @Input() set list(value: any[]) {
    if (!!value && !!this._list && this._list.length != value.length) {
      this.selectedIndex = null;
      this.groupSelected.emit(null);
    }
    else if (this.selectedIndex != null) {
      this.groupSelected.emit(value[this.selectedIndex]);
    }
    this._list = value;
  }
  @Input() displayField: string = 'name';
  @Input() secondaryField: string = null;

  @Output() groupSelected = new EventEmitter<any>();
  constructor() { }

  ngOnInit(): void {
  }

  isSelected(index: number): boolean {
    return this.selectedIndex == index;
  }

  onItemClick(item: any, index: number) {
    if (this.selectedIndex != index) {
      this.groupSelected.emit(item);
      this.selectedIndex = index;
    }
  }
}
