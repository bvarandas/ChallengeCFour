import { RegistersComponent } from './components/registers/registers.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CashflowComponent } from './components/cashflow/cashflow.component';

const routes: Routes = [{
  path:'registers', component: RegistersComponent
},{
  path:'cashflow', component: CashflowComponent
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
