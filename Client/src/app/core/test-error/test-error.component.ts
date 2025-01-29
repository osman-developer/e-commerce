import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from '../../../../environment';

@Component({
  selector: 'app-test-error',
  standalone: false,

  templateUrl: './test-error.component.html',
  styleUrl: './test-error.component.scss',
})
export class TestErrorComponent implements OnInit {
  baseUrl = environment.apiUrl;
  validationErrors: any;

  constructor(private httpClient: HttpClient) {}
  ngOnInit() {}

  get404Error() {
    this.httpClient.get(this.baseUrl + 'products/42').subscribe({
      next: (response: any) => console.log(response),
      error: (error) => console.log(error),
    });
  }
  get500Error() {
    this.httpClient.get(this.baseUrl + 'buggy/servererror').subscribe({
      next: (response: any) => console.log(response),
      error: (error) => console.log(error),
    });
  }
  get400Error() {
    this.httpClient.get(this.baseUrl + 'buggy/badrequest').subscribe({
      next: (response: any) => console.log(response),
      error: (error) => console.log(error),
    });
  }
  get400ValidationError() {
    this.httpClient.get(this.baseUrl + 'products/for').subscribe({
      next: (response: any) => console.log(response),
      error: (error) => (
        console.log(error), (this.validationErrors = error.errors)
      ),
    });
  }
}
