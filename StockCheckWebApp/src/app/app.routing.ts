import { Routes, RouterModule } from '@angular/router';
import { StockComponent } from './stock/stock.component';

const appRoutes: Routes = [
    { path: 'stock', component: StockComponent },
    { path: '', component: StockComponent},

    // otherwise redirect to home
    { path: '**', redirectTo: '',  }
];

export const routing = RouterModule.forRoot(appRoutes);
