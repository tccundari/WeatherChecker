import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<WeatherForecast[]>(baseUrl + 'WeatherChecker').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
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
