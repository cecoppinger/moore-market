<template>
    <div class='container-fluid moore-gradient moore-navy'>
        <table class='table'>
            <thead>
                <tr>
                    <th scope='col'>ID</th>
                    <th scope='col'>Name</th>
                    <th scope='col'>Category</th>
                    <th scope='col'>Price</th>
                    <th scope='col'>Quantity</th>
                    <th scope='col'>Edit/Delete</th>    
                </tr>
            </thead>
            <tbody>
                <farmer-product-item v-for='product in vendorProducts.products' :key='product.id' :product='product'> </farmer-product-item>

            </tbody>
        </table>
    </div>
</template>

<script>
import FarmerProductItem from './FarmerProductItem.vue'
import { mapActions, mapState } from 'vuex'

export default {
    name: 'FarmerProductDash',
    components: {
        FarmerProductItem
    },
    
    data() {
        return {
        }
    },

    computed: {
        ...mapState('vendor', [
          'vendorProducts'
        ]),
    },

    methods: {
        ...mapActions('vendor', [
            'getVendorProducts'
        ])
    },

    methods: {
        ...mapActions('product', [
            'getAllProductsFromApi'
        ]),
        

    },

    async created() {
        await this.getAllProductsFromApi()
        await this.getVendorProducts(this.currentUser.id)
        
    },
}
</script>

<style>

</style>
