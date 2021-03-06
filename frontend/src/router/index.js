import Vue from 'vue'
import Router from 'vue-router'

import HelloWorld from '../components/HelloWorld.vue'
import Login from '../components/Login.vue'
import Dashboard from '../components/Dashboard.vue'
import ProductHomePage from '../components/Product/ProductHomepage.vue'
import ManageCategories from '../components/Product/ManageCategories.vue'
import AddProduct from  '../components/Product/AddProduct.vue'
import EditProduct from '../components/Product/EditProduct.vue'
import SingleProduct from '../components/Product/SingleProduct.vue'
import ProductSearchResult from '../components/Product/ProductSearchResult.vue'
import BrowseFarmers from '../components/BrowseFarmers.vue'
import BrowseInventory from '../components/BrowseInventory.vue'
import AllProducts from '../components/Product/AllProducts.vue'
import SearchProducts from '../components/Product/SearchProducts.vue'
import ShoppingCart from '../components/ShoppingCart.vue'
import RegisterUser from '../components/RegisterUser.vue'

Vue.use(Router)

export const router = new Router({
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
    name:'ProductHomePage',
    path: '/product',
    component: ProductHomePage,
  }, {
    name:'ManageCategories',
    path: '/categories',
    component: ManageCategories
  }, {
    path: '/product/add',
    name: 'AddProduct',
    component: AddProduct,
  }, {
    name: 'SingleProduct',
    path: '/product/:id',
    component: SingleProduct
  }, {
    name: 'ProductSearchResult',
    path: '/productsearchresult',
    component: ProductSearchResult
  }, {
    name: 'EditProduct',
    path: '/product/edit/:id',
    component: EditProduct
  }, {
    name: 'BrowseFarmers',
    path: '/farmers',
    component: BrowseFarmers,
  }, {
    name: 'BrowseInventory',
    path: '/farmers/:id',
    component: BrowseInventory,
    props: true,
  }, {
    name: 'AllProducts',
    path: '/products/all',
    component: AllProducts,
  }, {
    name: 'SearchProducts',
    path: '/products/search',
    component: SearchProducts,
  }, {
    name: 'ShoppingCart',
    path: '/products/cart',
    component: ShoppingCart,
  }, {
    name: 'Login',
    path: '/login',
    component: Login
  }, {
    name: 'Register',
    path: '/register',
    component: RegisterUser,
  }
  ],
});

router.beforeEach((to, from, next) => {
  const allowedRoutes = ['/login', 
      '/register', 
      '/dashboard', 
      '/products/all', 
      '/products/search',
      '/farmers',
      '/product'
  ]
  const allowedVendorRoutes = [
      '/product/edit',
      '/product/add',
      '/categories',
  ]
  const user = JSON.parse(sessionStorage.getItem('user'))
  const authRequired = !allowedRoutes.includes(to.path)
  const vendorRequired = allowedVendorRoutes.includes(to.path)

  if((authRequired && !user) || (vendorRequired && user.accountType === 'user')) {
    next('/login')
  }

  next()
});


