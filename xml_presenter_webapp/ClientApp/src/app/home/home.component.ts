import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Order } from 'src/models/Order';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  
  orderNumber = '';
  order: Order;
  showError = false;
  

  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {}


  onKey(event: any) { // without type info
    if (event.keyCode == 13) {
      //fetch on enter
      this.fetchOrder();
    } else {
      this.orderNumber = event.target.value;
    }
  }
  
  fetchOrder() {
    
    if(this.orderNumber.length <= 0) {
      return;
    }
    //fetching specific order
    this.http.get<Order>(this.baseUrl + 'order/orderid/' + this.orderNumber).subscribe(result => {
      console.log(result);
      this.order = result;
      this.showError = result == null;
    }, error => console.error(error));
  }
}
