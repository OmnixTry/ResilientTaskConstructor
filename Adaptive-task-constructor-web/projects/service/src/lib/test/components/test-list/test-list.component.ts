import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { SearchService } from 'projects/teacher/src/lib/group/search.service';
import { concat, debounceTime, merge, Observable, of, skipWhile, switchMap } from 'rxjs';
import { GroupTest } from '../../../group/entity/groupTest';

@Component({
  selector: 'srvc-test-list',
  templateUrl: './test-list.component.html',
  styleUrls: ['./test-list.component.scss']
})
export class TestListComponent implements OnInit {
  searchForm: FormGroup;
  searchResult: Observable<GroupTest[]>;
  @Input() startSearchImmediately: boolean = false;
  @Input() excludeIds: number[];
  @Output() testSelected = new EventEmitter<GroupTest>();

  constructor(private fb: FormBuilder, private searchService: SearchService) { }

  ngOnInit(): void {
    this.searchService.findTest('', '');
    this.searchForm = this.fb.group({
      name: [''],
      topic: ['']
    });
    const refreshSearch = this.searchService.refreshSearch.pipe(switchMap(x => of({
      name: this.searchForm.get('name').value,
      topic: this.searchForm.get('topic').value,
    })));
    const source = this.startSearchImmediately ? merge(of({ name: '', topic: '' }), this.searchForm.valueChanges, refreshSearch) : this.searchForm.valueChanges;
    this.searchResult = source.pipe(
      debounceTime(300),
      switchMap(v => this.searchService.findTest(v.name, v.topic)),
      switchMap(v => {
        if (!!this.excludeIds && this.excludeIds.length > 0)
          return of(v.filter(t => !this.excludeIds.includes(t.id)))
        else
          return of(v)
      }));
  }

  onTestSelected(test: GroupTest) {
    this.testSelected.emit(test);
  }
}
