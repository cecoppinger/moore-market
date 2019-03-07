<template>
  <div class="container-fluid">
    <form @submit.prevent="validateForm"> 

      <div class="form-group row justify-content-center">
        <label for="name" class="col-md-2 col-form-label left-align">Name:</label>
        <div class="col-md-4">
          <input id="name" class="form-control" placeholder="Product Name" v-model.trim="product.name" />
        </div>
      </div>

      <div v-if="errors.nameError" class="col-md-6 centered alert alert-danger">{{ errors.nameErrorMsg }}</div>

      <div class="form-group row align-items-center justify-content-center">
        <label for="description" class="col-md-2 col-form-label left-align">Description:</label>
        <div class="col-md-4">
          <textarea id="description" class="form-control" placeholder="Product Description" v-model="product.description"></textarea>
        </div>
      </div>

      <div v-if="errors.descriptionError" class="col-md-6 centered alert alert-danger">{{ errors.descriptionErrorMsg }}</div>

      <div class="form-group row justify-content-center">
        <label for="category" class="col-md-2 col-form-label left-align">Category:</label>
        <div class="col-md-4">
          <select id="category" class="form-control" v-model="product.categoryId">
            <option disabled :value='null'> Please Select One </option>
            <option v-for='category in allCategories' :key='category.Id' :value='category.id'> {{category.name}} </option>
          </select>
        </div>
      </div>

      <div v-if="errors.categoryError" class="col-md-6 centered alert alert-danger">{{ errors.categoryErrorMsg }}</div>

      <div class="form-group row justify-content-center">
        <label for="quantity" class="col-md-2 col-form-label left-align">Quantity:</label>
        <div class="col-md-4">
          <input id="quantity" type="number" class="form-control" v-model.number="product.quantity" />
        </div>
      </div>

      <div class="form-group row justify-content-center">
        <label for="price" class="col-md-2 col-form-label left-align">Price:</label>
        <div class="col-md-4">
          <input id="price" type="number" class="form-control" v-model.number="product.price" step="0.01" />
        </div>
      </div>

      <div v-if="errors.priceError" class="col-md-6 centered alert alert-danger">{{ errors.priceErrorMsg }}</div>

      <fieldset class="form-group">
        <div class="row align-items-center justify-content-center">
          <legend class="col-md-2 col-form-label left-align">Is it perishable?</legend>
          <div class="custom-control custom-radio col-md-2">
            <input class="custom-control-input" type="radio" id="isPerishableYes" name="isPerishable" value="1" v-model.number='product.isPerishable' />
            <label class="custom-control-label" for="isPerishableYes"> Yes </label>
          </div>
          <div class="custom-control custom-radio col-md-2">
            <input class="custom-control-input" type="radio" id="isPerishableNo" name="isPerishable" value="0" v-model.number='product.isPerishable' />
            <label class="custom-control-label" for="isPerishableNo" >No </label>
          </div>
        </div>
      </fieldset>

      <div v-show="product.isPerishable">
        <fieldset class="form-group">
          <div class="row align-items-center justify-content-center">
            <legend class="col-md-2 col-form-label left-align"> Keep in dry storage? </legend>
            <div class="custom-control custom-radio col-md-2">
              <input class="custom-control-input" type="radio" id="isRoomTempYes" name="isRoomTemp" :value="1" v-model='product.keepRoom' />
              <label class="custom-control-label" for="isRoomTempYes"> Yes </label>
            </div>
            <div class="custom-control custom-radio col-md-2">
              <input class="custom-control-input" type="radio" id="isRoomTempNo" name="isRoomTemp" :value="0" v-model='product.keepRoom' />
              <label class="custom-control-label" for="isRoomTempNo"> No </label>
            </div>
          </div>
        </fieldset>

        <div v-show="product.keepRoom">
          <div class="form-group row align-items-center justify-content-center">
            <label for="name" class="col-md-2 col-form-label left-align"> Dry Storage Shelf Life: </label>
            <div class="col-md-4">
              <input id="name" class="form-control" v-model.trim="product.shelfLifeRoom" />
            </div>
          </div>
          <div v-if="errors.keepRoomError" class="col-md-6 centered alert alert-danger">{{ errors.keepRoomErrorMsg }}</div>
        </div>

        <fieldset class="form-group">
          <div class="row align-items-center justify-content-center">
            <legend class="col-md-2 col-form-label left-align"> Keep in cooler? <br /> (At or below 40° F (4° C)) </legend>
            <div class="custom-control custom-radio col-md-2">
              <input class="custom-control-input" type="radio" id="isFridgeYes" name="isFridge" :value="1" v-model.number='product.keepFridge' />
              <label class="custom-control-label" for="isFridgeYes"> Yes </label>
            </div>
            <div class="custom-control custom-radio col-md-2">
              <input class="custom-control-input" type="radio" id="isFridgeNo" name="isFridge" :value="0" v-model.number='product.keepFridge' />
              <label class="custom-control-label" for="isFridgeNo"> No </label>
            </div>
          </div>
        </fieldset>

        <div v-show="product.keepFridge">
          <div class="form-group row align-items-center justify-content-center">
            <label for="name" class="col-md-2 col-form-label left-align">Cooler Shelf Life:</label>
            <div class="col-md-4">
              <input id="name" class="form-control" v-model="product.shelfLifeFridge" />
            </div>
          </div>
        </div>

        <fieldset class="form-group">
          <div class="row align-items-center justify-content-center">
            <legend class="col-md-2 col-form-label left-align"> Keep in freezer? <br /> (At or below 0° F (-18° C))</legend>
            <div class="custom-control custom-radio col-md-2">
              <input class="custom-control-input" type="radio" id="isFreezerYes" name="isFreezer" :value='1' v-model.number='product.keepFreezer' />
              <label class="custom-control-label" for="isFreezerYes"> Yes </label>
            </div>
            <div class="custom-control custom-radio col-md-2">
              <input class="custom-control-input" type="radio" id="isFreezerNo" name="isFreezer" :value='0' v-model.number='product.keepFreezer' />
              <label class="custom-control-label" for="isFreezerNo"> No </label>
            </div>
          </div>
        </fieldset>

        <div v-show="product.keepFreezer">
          <div class="form-group row align-items-center justify-content-center">
            <label for="name" class="col-md-2 col-form-label left-align">Freezer Shelf Life:</label>
            <div class="col-md-4">
              <input id="name" class="form-control" v-model="product.shelfLifeFreezer" />
            </div>
          </div>
        </div>

      </div> 

      <div class="form-group">
          <div class='row align-items-center justify-content-center'>
              <label for='pic' class="col-md-2 col-form-label left-align"> Upload a Picture: </label>
              <div class="col-md-4">
                  <input class="form-control-file" type='file' name='pic' accept='image/*' id='pic' @change='generateBase64'>
              </div>
          </div>

          <div v-if='product.image'> 
              <div class='row align-items-center justify-content-center'>
                  <label class="col-md-2 col-form-label left-align"> Uploaded Picture: </label>
                  <div class="col-md-4">
                      <img :src='product.image' class='image img-thumbnail' id='displayedImage'>
                  </div>
              </div>
          </div>
      </div>  

      <div class="center-align">
        <button class="btn btn-primary" type="submit">Submit</button>
      </div>
      
    </form>
  </div>
</template>

<script>
import {mapState, mapActions} from 'vuex'

export default {
  name: 'ProductForm',
  props: ['formType', 'productToEdit'],
  computed: {
    ...mapState('category', [
      'allCategories'
    ]),
    ...mapState('user', [
      'currentUser'
    ])
  },
  methods: {
    ...mapActions('product', [
      'addNewProduct',
      'editProductById'
    ]),
    ...mapActions('category', [
      'getAllCategoriesFromApi'
    ]),
    async validateForm() {
      let noErrorMsg = ''

      if(this.product.name === '') {
        this.errors.nameErrorMsg = 'You must provide a name'
        this.errors.nameError = true
      } else {
        this.errors.nameErrorMsg = noErrorMsg
        this.errors.nameError = false
      }
      if(this.product.description === '') {
        this.errors.descriptionErrorMsg = 'You must provide a description'
        this.errors.descriptionError = true
      } else {
        this.errors.descriptionErrorMsg = noErrorMsg
        this.errors.descriptionError = false
      }
      if(this.product.categoryId === 0) {
        this.errors.categoryErrorMsg = 'You must choose a category'
        this.errors.categoryError = true        
      } else {
        this.errors.categoryErrorMsg = noErrorMsg
        this.errors.categoryError = false
      }
      if(this.product.price === 0) {
        this.errors.priceErrorMsg = 'Please set a price'
        this.errors.priceError = true         
      } else {
        this.errors.priceErrorMsg = noErrorMsg
        this.errors.priceError = false
      }
      if(this.product.isPerishable) {
        let errorMsg = 'Please provide a shelf life'
        if(this.keepRoom && this.product.shelfLifeRoom === '') {
          this.errors.keepRoomErrorMsg = errorMsg
          this.errors.keepRoomError = true
        } else {
          this.errors.keepRoomErrorMsg = noErrorMsg
          this.errors.keepRoomError = false
        }
        if(this.keepFridge && this.product.shelfLifeFridge === '') {
          this.errors.keepFridgeErrorMsg = errorMsg
          this.errors.keepFridgeError = true
        } else {
          this.errors.keepFridgeErrorMsg = noErrorMsg
          this.errors.keepFridgeError = false
        }
        if(this.keepFreezer && this.product.shelfLifeFreezer === '') {
          this.errors.keepFreezerErrorMsg = errorMsg          
          this.errors.keepFreezerError = true
        } else {
          this.errors.keepFreezerErrorMsg = noErrorMsg
          this.errors.keepFreezerError = false
        }
        if(this.product.image.length > 999999) {
          this.errors.imageErrorMsg = 'Image file is too large'
          this.errors.imageError = true
        } else {
          this.errors.imageErrorMsg = noErrorMsg
          this.errors.imageError = false
        }
      }
      if(!this.checkErrors()) {
        this.product.userId = this.currentUser.id
        if(this.formType === 'add') {
          let res = await this.addNewProduct(this.product)
          console.log(res)
          if (res.data) {
            let productId = res.data.id
            this.$router.push({ path: `/product/${productId}`})
          }
        }

        if(this.formType === 'edit') {
          let res = await this.editProductById(this.product)
          
          if (res.data) {
            let productId = res.data.id
            this.$router.push({ path: `/product/${productId}`})
          }
        }
      }

      //if we make it here there must be an error
      return false
    },

    checkErrors() {
      if (this.errors.nameError ||
      this.errors.descriptionError ||
      this.errors.categoryError ||
      this.errors.priceError ||
      this.errors.keepRoomError ||
      this.errors.keepFridgeError ||
      this.errors.keepFreezerError ||
      this.errors.imageError) {
        return true
      }

      return false
    },

    generateBase64(event) {

      let reader = new FileReader()
      reader.readAsDataURL(event.target.files[0])
      
      reader.onload = () => {
              this.product.image = reader.result
              }
    },
  },

  data() {
    return {
      product: {
        name:'',
        description: '',
        categoryId: 0,
        quantity: 0,
        price: 0,
        isPerishable: 0,
        keepRoom: 0,
        shelfLifeRoom: '',
        keepFridge: 0,
        shelfLifeFridge: '',
        keepFreezer: 0,
        shelfLifeFreezer: '',
        image: '',
      },
      errors: {
        any: false,
        nameError: false,
        nameErrorMsg: '',
        descriptionError: false,
        descriptionErrorMsg: '',
        categoryError: false,
        categoryErrorMsg: '',
        priceError: false,
        priceErrorMsg: '',
        keepRoomError: false,
        keepRoomErrorMsg: '',
        keepFridgeError: false,
        keepFridgeErrorMsg: '',
        keepFreezerError: false,
        keepFreezerErrorMsg: '',
        imageError: false,
        imageErrorMsg: '',
      },
    }
  },

  async created(){
    await this.getAllCategoriesFromApi()
    if (this.formType === 'edit') {
      this.product = this.productToEdit
    }
  }
}
</script>

<style scoped>
.left-align {
  text-align: left; 
}
.center-align {
  text-align: center;
}
li {
  list-style: none;
}
.centered {
  margin-left: auto;
  margin-right: auto;
  margin-bottom: 10px;
}
</style>
