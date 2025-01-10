import { Component, Input, Output } from '@angular/core';
import { EventEmitter } from '@angular/core';

@Component({
  selector: 'app-pager',
  standalone: false,

  templateUrl: './pager.component.html',
  styleUrl: './pager.component.scss',
})
export class PagerComponent {
  @Input() totalCount!: number;
  @Input() pageSize!: number;
  @Output() pageChanged: EventEmitter<number> = new EventEmitter();
  
  onPagerChange(event: any) {
    this.pageChanged.emit(event.page);
  }
}
