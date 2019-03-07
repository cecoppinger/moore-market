<template>
  <div class='container-fluid moore-gradient moore-navy'>

    <div class='p-3'>

      <h2> Edit Product </h2>

    </div>

    <product-form :productToEdit='getProductById(productId)' :formType='formType'> </product-form>

  </div>
</template>

<script>
import {mapActions, mapGetters} from 'vuex'


import ProductForm from './ProductForm.vue'

export default {
  name: 'EditProduct',

  components: {
    ProductForm
  },

  props: [],

  data() {
    return {
      productId: parseInt(this.$route.params.id),
      formType: 'edit',
    }
  },   
  computed: {
      ...mapGetters('product', ['getProductById']),

    },
  methods: {
    ...mapActions('product', [
      'getAllProductsFromApi'
    ])
  },
  async created() {
    await this.getAllProductsFromApi()
  }
}

</script>

<style>

</style>
