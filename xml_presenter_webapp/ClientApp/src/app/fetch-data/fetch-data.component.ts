import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Order } from 'src/models/Order';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: Order[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    //fetching all orders
    http.get<Order[]>(baseUrl + 'order').subscribe(result => {
      this.forecasts = result;
      console.log(result);
    }, error => console.error(error));
  }
}


