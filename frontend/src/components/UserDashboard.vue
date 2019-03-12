<template>
    <div>
      <h1>This will be the User Dashboard</h1>

      <p>Your current position: Latitude({{latitude}}), Longitude({{longitude}})</p>
      <button class="btn btn-primary mb-1" @click="showFeed = !showFeed">Toggle Feed box</button>
      <UserFeed v-show="showFeed" :allMessages="allMessages"></UserFeed>

      <div class="row">
        <div class="col-8">
          <h4>Product List</h4>
          <ul class="list-unstyled">
            <li v-for="product in allProducts" :key="product.id" class="media text-left border border-success mx-1 my-2">
              <img src="https://via.placeholder.com/64" class="mr-3">
              <div class="media-body">
                <h5 class="mt-0 mb-1"><router-link :to="{name: 'SingleProduct', params: { id: product.id },}">{{ product.name }}</router-link>  ${{Number(product.price).toFixed(2)}}</h5>
                {{ product.description }}
              </div>
            </li>
          </ul>
        </div>
        <div class="col-4"> 
<<<<<<< HEAD
          <h4>Vendor List</h4>
          <div v-for="vendor in allVendors" :key="vendor.id" class="card mx-2 my-2">
=======
          <h4>Farmer List</h4>
          <div v-for="farmer in allVendors" :key="farmer.id" class="card mx-2 my-2">
>>>>>>> 17468e5faeb1225d77387d0ebff1da3716bed060
            <img src="https://via.placeholder.com/75x35?text=farmer+logo" width="50" class="card-img-top" >
            <div class="card-body">
              <h5 class="card-title">{{ vendor.username }}</h5>
              <p class="card-text">Brief description of farmer's inventory, address or area of town</p>
              <router-link :to="{name: 'BrowseInventory', params: { id: vendor.id },}"       
                class="btn btn-primary">Visit {{ vendor.username}}'s storefront</router-link>
            </div>
          </div>
        </div>
      </div>  
    </div>
</template>

<script>
import UserFeed from '../components/UserFeed.vue'
import { mapActions, mapState } from 'vuex'

export default {
  name: 'userdashboard',
  components: {
      UserFeed
  },
  data() {
    return {
      error: '',
      latitude: '',
      longitude: '',
      showFeed: true
      }
  },

  created() {
    this.getAllVendors(),
<<<<<<< HEAD
    this.getAllMessagesFromApi()
    //this.getAllProductsFromApi()
=======
    this.getAllProductsFromApi()
>>>>>>> 17468e5faeb1225d77387d0ebff1da3716bed060
  },

  mounted(){
    this.getLocation()
  },

  computed: {
    ...mapState('product', ['allProducts']),
<<<<<<< HEAD
    ...mapState('vendor', ['allVendors', 'allMessages'])
  },

  methods: {
    //...mapActions('product', ['getAllProductsFromApi']),
    ...mapActions('vendor', ['getAllVendors', 'getAllMessagesFromApi']),
=======
    ...mapState('vendor', ['allVendors'])
  },

  methods: {
    ...mapActions('product', ['getAllProductsFromApi']),
    ...mapActions('vendor', ['getAllVendors']),
>>>>>>> 17468e5faeb1225d77387d0ebff1da3716bed060
    getLocation() {
      if(navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(this.showPosition);
      } else {
        this.error = "Geolocation is not supported";
      }
    },  
    showPosition(position) {
        this.latitude = position.coords.latitude;
        this.longitude = position.coords.longitude;
      }
    }
  }  


</script>

