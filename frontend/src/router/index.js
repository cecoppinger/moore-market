import Vue from 'vue'
import Router from 'vue-router'

import HelloWorld from '../components/HelloWorld.vue'
import Dashboard from '../components/Dashboard.vue'
import AddProduct from  '../components/AddProduct.vue'
import ProductForm from '../components/ProductForm.vue'
import EditProduct from '../components/EditProduct.vue'

Vue.use(Router)

export default new Router({
  mode: 'history',
  routes: [{
    name: 'Homepage',
    path: '/',
    component: HelloWorld,
  }, {
    name: 'Dashboard',
    path: '/dashboard',
    component: Dashboard,
  }, {
    path: '/addproduct',
    name: 'AddProduct',
    component: AddProduct,
  }, {
    path: '/productform',
    component: ProductForm,
  }, {
    name: 'EditProduct',
    //:id is a paramater for this route that we can pass in
    path: '/editproduct/:id',
    component: EditProduct,
    //this signifies that we can pass in paramaters as a prop on this component
    props: true,
  }],
})