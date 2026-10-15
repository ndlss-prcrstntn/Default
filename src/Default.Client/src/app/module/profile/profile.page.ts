import { Component, inject, OnInit, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {
  IonContent,
  IonHeader,
  IonTitle,
  IonToolbar,
} from '@ionic/angular/standalone';
import { API_BASE_URL } from '../../api-base-url';

interface VersionInfo {
  version: string;
  gitSha?: string | null;
  environment: string;
}

@Component({
  selector: 'app-profile',
  templateUrl: './profile.page.html',
  styleUrls: ['./profile.page.scss'],
  imports: [IonHeader, IonToolbar, IonTitle, IonContent],
})
export class ProfilePage implements OnInit {
  private readonly http = inject(HttpClient);
  private readonly apiBase = inject(API_BASE_URL);

  readonly version = signal<string>('…');

  ngOnInit(): void {
    this.http.get<VersionInfo>(`${this.apiBase}/version`).subscribe({
      next: (v) =>
        this.version.set(
          `${v.version}${v.gitSha ? ` (${v.gitSha.slice(0, 7)})` : ''} · ${v.environment}`,
        ),
      error: () => this.version.set('unavailable'),
    });
  }
}
