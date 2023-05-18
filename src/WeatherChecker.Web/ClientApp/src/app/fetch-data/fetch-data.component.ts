import { Component, Inject, ViewChild, ElementRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NotificationService } from "../notification.service";
import * as XLSX from 'xlsx';

interface State {
  value: string;
  viewValue: string;
}

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})

export class FetchDataComponent {
  @ViewChild('WeatherResults', { static: false }) table: ElementRef;
  public forecasts: WeatherForecast[];
  public forecasts_cache: WeatherForecast[];
  private apiUrl: string;
  private httpCli: HttpClient;
  private notificationServ: NotificationService;
  

  states: State[] = [
    { value: 'all', viewValue: 'All' },
    { value: 'nsw', viewValue: 'New South Wales' },
    { value: 'vic', viewValue: 'Victoria' },
    { value: 'qld', viewValue: 'Queensland' },
    { value: 'wa', viewValue: 'Western Australia' },
    { value: 'sa', viewValue: 'South Australia' },
    { value: 'tas', viewValue: 'Tasmania' },
    { value: 'act', viewValue: 'Australian Capital Territory' },
    { value: 'nt', viewValue: 'Northern Territory' }
  ];

  selectedState(event: Event) {
    this.getWeatherInfo((event.target as HTMLSelectElement).value);
  }

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, _notificationSvc: NotificationService) {
    this.apiUrl = baseUrl;
    this.httpCli = http;
    this.notificationServ = _notificationSvc;

    this.getWeatherInfo();
  }

  getWeatherInfo(state: string = '') {
    var url = this.apiUrl + 'WeatherChecker';

    if (state != 'all' && state != '')
      url = url + '/' + state;

    this.httpCli.get<WeatherForecast[]>(url).subscribe(result => {
      this.forecasts = result;
      this.forecasts_cache = this.forecasts;
    }, error => this.notificationServ.error('Can\'t reach the API', error.message));
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.forecasts = this.forecasts_cache;
    this.forecasts = this.forecasts.filter(x => x.descriptionCity.toLowerCase().includes(filterValue.toLowerCase()));
  }

  ExportToExcel() {
    alert(this.table);
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(this.table.nativeElement);
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');

    /* save to file */
    XLSX.writeFile(wb, 'WeatherInformation.xlsx');

  }
}

interface WeatherForecast {
  descriptionCity: string;
  min: string;
  max: number;
  precision: number;
  obsNow: string;
  obsLow: string;
  obsLowTime: string;
  obsHigh: string;
  obsHighTime: string;
  obsRain: string;
}
