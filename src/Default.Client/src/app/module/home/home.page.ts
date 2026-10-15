import { Component, inject, OnInit, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {
  IonContent,
  IonHeader,
  IonTitle,
  IonToolbar,
} from '@ionic/angular/standalone';
import { API_BASE_URL } from '../../api-base-url';

@Component({
  selector: 'app-home',
  templateUrl: './home.page.html',
  styleUrls: ['./home.page.scss'],
  imports: [IonHeader, IonToolbar, IonTitle, IonContent],
})
export class HomePage implements OnInit {
  private readonly http = inject(HttpClient);
  private readonly apiBase = inject(API_BASE_URL);

  readonly ping = signal<string>('…');

  ngOnInit(): void {
    this.http.get<{ status: string }>(`${this.apiBase}/Ping`).subscribe({
      next: (r) => this.ping.set(r.status),
      error: () => this.ping.set('unreachable'),
    });
  }
}
