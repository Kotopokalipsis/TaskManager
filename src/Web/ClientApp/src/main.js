import Vue from 'vue'
import App from './App.vue'
import Router from "vue-router";
import SuiVue from 'semantic-ui-vue';
import DatePicker from 'v-calendar/lib/components/date-picker.umd'
import Multiselect from 'vue-multiselect'

import './assets/css/app.css'
import 'semantic-ui-css/semantic.min.css';
import 'vue-multiselect/dist/vue-multiselect.min.css';

import Project from "./components/projects/Project.vue";
import Employer from "./components/employees/Employer.vue";
import Task from "./components/tasks/Task.vue";
import ProjectsList from "./components/projects/list/ProjectsList.vue";
import EmployeesList from "./components/employees/list/EmployeesList.vue";
import NotFoundPage from "./components/exceptions/NotFound.vue";
import HomePage from "./components/home/Home.vue";
import TasksList from "./components/tasks/list/TasksList.vue";

Vue.config.productionTip = false

Vue.use(Router)
Vue.use(SuiVue);
Vue.component('multiselect', Multiselect);
Vue.component('date-picker', DatePicker)

const routes = [
  { path: '/', component: HomePage },
  { path: '/projects', component: ProjectsList, name: "project-list" },
  { path: '/projects/:id/update', component: Project, name: "update-project" },
  { path: '/projects/create', component: Project },
  { path: '/employees', component: EmployeesList, name: "employees-list" },
  { path: '/employees/create', component: Employer },
  { path: '/employees/:id/update', component: Employer, name: "update-employer"},
  { path: '/tasks', component: TasksList, name: "tasks-list" },
  { path: '/tasks/create', component: Task },
  { path: '/tasks/:id/update', component: Task, name: "update-task"},
  { path: '*', component: NotFoundPage, name: "not-found" }
]

const router = new Router({
  mode: 'history',
  routes
})

new Vue({
  router,
  render: h => h(App),
}).$mount('#app')
