import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { DateAdapter, MAT_DATE_FORMATS } from '@angular/material/core';
import { APP_DATE_FORMATS, DateHelper } from '../../helpers/date.helper';
import { Subscription } from 'rxjs';
import { ApplicationService } from '../../services';
import { TranslateService } from '@ngx-translate/core';
import { isNullOrUndefined, startOfDay } from '../../helpers/application.helper';

@Component({
  selector: 'app-date-input',
  templateUrl: './date-input.component.html',
  styleUrls: ['./date-input.component.scss'],
  providers: [
    { provide: DateAdapter, useClass: DateHelper },
    { provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS }
  ]
})
export class DateInputComponent {
  @Input() form: FormGroup;
  @Input() controlName: string;
  @Input() groupName: string;
  @Input() placeholderToTranslate: string;
  @Input() tooltip: string;
  @Input() required: boolean = false;
  @Input() chosenDate: Date;

  private readonly subscription: Subscription = new Subscription();

  constructor(private dateAdapter: DateAdapter<Date>,
              private appService: ApplicationService,
              private translate: TranslateService) {
    this.listenForLanguageChange();
  }

  customDateFilter = (date: Date): boolean => {
    return date >= startOfDay(new Date()) ||
      (!isNullOrUndefined(this.chosenDate) && startOfDay(date).toDateString() === startOfDay(this.chosenDate).toDateString());
  }

  private listenForLanguageChange(): void {
    this.subscription.add(this.appService.appLangState.subscribe(res => {
      this.translate.use(res);
      this.dateAdapter.setLocale(res);
    }));
  }
}
