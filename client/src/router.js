import Vue from 'vue'
import Router from 'vue-router'
import Editor from './views/Editor.vue'
import Test from "./views/Test.vue";
import Home from "./views/Home.vue";

Vue.use(Router);

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    { path: '/', name: 'Home', component: Home },
    { path: '/editor/:id?', name: 'editor', component: Editor },
    { path: '/test', name: 'test', component: Test }
  ]
})
