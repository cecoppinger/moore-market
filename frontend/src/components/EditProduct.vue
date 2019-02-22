<template>
  <div class="edit">
    <div class="product-form">
      <ProductForm 
      :productToEdit="getProductById(id)"
      @formValid="editedProduct => sendEdit(editedProduct)" />
    </div>
  </div>
</template>

<script>
import { mapActions, mapGetters } from 'vuex'
import ProductForm from './ProductForm'

export default {
  name: 'EditProduct',
  components: {
    ProductForm
  },
  props: {
    id: {
      type: [Number, String],
      required: true,
      validator(value) {
        return Number.isInteger(value)
      },
    },
  },
  methods: {
    ...mapActions('product', ['editProductById']),
    sendEdit(editedProduct) {
      this.editProductById(editedProduct)
      this.$router.push('/dashboard')
    }
  },
  computed: {
    ...mapGetters('product', ['getProductById']),
  },
}
</script>

<style>

</style>
