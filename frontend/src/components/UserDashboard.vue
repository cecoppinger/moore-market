<template>
    <div>
      <h1>This will be the User Dashboard</h1>

      <p>Your current position: Latitude({{latitude}}), Longitude({{longitude}})</p>
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
          <h4>Farmer List</h4>
          <div v-for="farmer in allVendors" :key="farmer.id" class="card mx-2 my-2">
            <img src="https://via.placeholder.com/75x35?text=farmer+logo" width="50" class="card-img-top" >
            <div class="card-body">
              <h5 class="card-title">{{ farmer.username }}</h5>
              <p class="card-text">Brief description of farmer's inventory, address or area of town</p>
              <router-link :to="{name: 'BrowseInventory', params: { id: farmer.id },}"       
                class="btn btn-primary">Visit {{ farmer.username}}'s storefront</router-link>
            </div>
          </div>
        </div>
      </div>  
    </div>
</template>

<script>
import { mapActions, mapState } from 'vuex'

export default {
  name: 'userdashboard',
  components: {
      
  },
  data() {
    return {
      error: '',
      latitude: '',
      longitude: ''
      }
  },

  created() {
    this.getAllVendors(),
    this.getAllProductsFromApi()
  },

  mounted(){
    this.getLocation()
  },

  computed: {
    ...mapState('product', ['allProducts']),
    ...mapState('vendor', ['allVendors'])
  },

  methods: {
    ...mapActions('product', ['getAllProductsFromApi']),
    ...mapActions('vendor', ['getAllVendors']),
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

