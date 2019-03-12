<template>
  <div class="browse-farmer-products container">
    <div class="farmer-details">
      <h3>{{ currentVendor.username }}'s Inventory</h3>
    </div>
    <div class="product-listing" v-for="(product, idx) in currentVendor.products" :key="idx">
      <product-search-item :product="product" :farmer="currentVendor"></product-search-item>
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
    ...mapActions('vendor', ['getVendor'])
  },
  created() {
    this.getVendor(this.id)
  }
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
