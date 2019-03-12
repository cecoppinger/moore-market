<template>
  <div class="vendor-list">
    <h2>Viewing Farmers</h2>
    <ul>
      <li 
      class="card"
<<<<<<< HEAD
      v-for="vendor in allVendors" :key="vendor.id"
=======
      v-for="farmer in allVendors" :key="farmer.id"
>>>>>>> 17468e5faeb1225d77387d0ebff1da3716bed060
      ><router-link
      :to="{
        name: 'BrowseInventory',
        params: { id: vendor.id },
      }" class="vendor-link">
        <div class="card-header">
          {{ vendor.username }}'s items
        </div>
        <div class="card-body">
          <p>Some short description about their store</p>
        </div>
      </router-link>
      </li>
    </ul>
  </div>
</template>

<script>
import { mapState, mapActions } from 'vuex'

export default {
  name: 'BrowseFarmers',
  methods: {
    ...mapActions('vendor', ['getAllVendors']),
  },
  created() {
    this.getAllVendors()
  },
  computed: {
    ...mapState('vendor', ['allVendors']),
  }
}
</script>

<style scoped>
ul {
  list-style-type: none;
}

h2 {
  margin-bottom: 30px;
}

.vendor-list {
  display: flex;
  flex-direction: column;
  width: 50%;
  margin: 0 auto;
}

.card {
  margin-bottom: 5px;
  box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
  transition: 0.3s;
}

.card:hover, .card:focus {
  box-shadow: 0 8px 16px 0 rgba(0,0,0,0.2);
}

.vendor-link {
  text-decoration: none;
  color: black;
}

</style>
