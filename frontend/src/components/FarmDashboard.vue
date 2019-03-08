<template>
  <div id="dashboard">
      <div>
          <h1>Vendor-Mart</h1>
      </div>

      <div class='row justify-content-center m-3'>

        <div class="card">
          <div class="card-body">
            <router-link to="/product/add"><button type="button" class="btn btn-success btn-lg mx-3">Add New Item</button></router-link>
            <router-link to='/categories'> <button type="button" class="btn btn-success btn-lg" > Manage Categories </button> </router-link>
          </div>
        </div>
        <FeedBox :vendorMessages="vendorMessages"></FeedBox>

      </div>
      <farmer-product-dash></farmer-product-dash>

  </div>
</template>

<script>
import FeedBox from '../components/FeedBox.vue';
import FarmerProductDash from '../components/Product/FarmerProductDash.vue'
import { mapActions, mapState } from 'vuex'
export default {
  name: 'FarmDashboard',
  components: {
      FeedBox,
      FarmerProductDash,
  },
  created() {
    this.getVendorMessages(this.currentUser.id)
  },
  computed: {
    ...mapState('user', ['currentUser']),
    ...mapState('vendor', ['vendorMessages'])
  },
  methods: {
    ...mapActions('vendor', ['getVendorMessages'])
    
  },
  data() {
      return {
          }
  }
}
</script>

<style>
#dashboard {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  text-align: center;
}
</style>