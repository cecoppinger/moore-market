<template>
  <div class="browse-farmer-products container">
    <div class="farmer-details">
      <h3>{{ currentVendor.username }}'s Inventory</h3>
    </div>
    <div class="product-listing" v-for="(product, idx) in currentVendor.products" :key="idx">
<<<<<<< HEAD
      <product-search-item :product="product" :vendor="currentVendor"></product-search-item>
=======
      <product-search-item :product="product" :farmer="currentVendor"></product-search-item>
>>>>>>> 17468e5faeb1225d77387d0ebff1da3716bed060
    </div>
  </div>
</template>

<script>
import { mapActions, mapState } from 'vuex'
import ProductSearchItem from './Product/ProductSearchItem'

export default {
  name: 'BrowseInventory',
  components: {
    ProductSearchItem,
  },
  props: {
    id: {
      type: [Number, String],
      validator(value) {
        return Number.isInteger(value)
      }
    },
  },
  computed: {
    ...mapState('vendor', ['currentVendor']),
  },
  methods: {
<<<<<<< HEAD
    ...mapActions('vendor', ['getVendor']),
  },
  created() {
    this.getVendor(this.id)
  },
=======
    ...mapActions('vendor', ['getVendor'])
  },
  created() {
    this.getVendor(this.id)
  }
>>>>>>> 17468e5faeb1225d77387d0ebff1da3716bed060
}
</script>

<style scoped>
.browse-farmer-products {
  display: flex;
  flex-wrap: wrap;
}

.farmer-details {
  flex-basis: 100%;
}

</style>
