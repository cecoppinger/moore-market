<template>
  <div id="dashboard">
      <div>
          <h1>Vendor-Mart</h1>
      </div>

      <div class="card-deck mx-2">
        <FeedBox :messages="messages"></FeedBox>

        <div class="card">
          <div class="card-body">
            <p><router-link to="/addproduct"><button type="button" class="btn btn-success btn-lg">Add New Item</button></router-link></p>
            <button type="button" class="btn btn-success btn-lg" disabled>Add New Category</button>
          </div>
        </div>
        <SummaryBox :sumBoxMessages="sumBoxMessages"></SummaryBox>
        
      </div>

      <div class=farmerNav>
      </div>
      
      <table class="table table-striped">
          <thead>
            <tr>
              <th :key='index' v-for="(columnTitle, index) in columnTitles">{{ columnTitle }}</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="item in allProducts" :key="item.id">
              <td>{{item.name}}</td>
              <td>{{item.category.name}}</td>
              <td>{{item.price}}</td>
              <td>{{item.quantity}}</td>
              <td>{{item.description}}</td>
              <td><router-link :to="{
                name: 'EditProduct',
                params: {
                  id: item.id,
                  }
                }"
                >
                Edit</router-link> | <button @click="removeProduct(item.id)">Remove</button></td>  
            </tr>
          </tbody>  
      </table>
  </div>
</template>

<script>
import FeedBox from '../components/FeedBox.vue';
import SummaryBox from '../components/SummaryBox.vue';
import { mapActions, mapState } from 'vuex'

export default {
  name: 'app',
  components: {
      'FeedBox': FeedBox,
      'SummaryBox': SummaryBox
  },
  created() {
    this.getAllProductsFromApi()
  },
  computed: {
    ...mapState('product', ['allProducts'])
  },
  methods: {
    ...mapActions('product', [
      'getAllProductsFromApi', 
      'removeProductById']),
    removeProduct(id) {
      this.removeProductById(id)
    },
  },
  data() {
      return {
          columnTitles: ['Item', 'Category','Price', 'Quantity', 'Item Code', 'Edit/Remove'],
          messages: [],
          sumBoxMessages: [],
          }
  }
}
</script>

<style>
#dashboard {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  text-align: center;
}

table.itemSummary {
  border: 2px solid #FFFFFF;
  width: 100%;
  text-align: center;
  border-collapse: collapse;
  padding: 10px;
}
table.itemSummary td, table.itemSummary th {
  border: 1px solid #FFFFFF;
  padding: 3px 4px;
}
table.itemSummary tbody td {
  font-size: 13px;
}
table.itemSummary td:nth-child(even) {
  background: #EBEBEB;
}
table.itemSummary thead {
  background: #FFFFFF;
  border-bottom: 4px solid #333333;
}
table.itemSummary thead th {
  font-size: 15px;
  font-weight: bold;
  color: #333333;
  text-align: center;
  border-left: 2px solid #333333;
}
table.itemSummary thead th:first-child {
  border-left: none;
}

table.itemSummary tfoot td {
  font-size: 14px;
}

.farmerNav {
    margin: 20px;
}
</style>
