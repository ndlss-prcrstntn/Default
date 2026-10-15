import { InjectionToken } from '@angular/core';

/** Absolute API origin (no trailing slash), e.g. http://localhost:5000 */
export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');
