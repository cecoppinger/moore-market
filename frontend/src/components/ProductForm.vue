<template>
  <div class="container-fluid edit-form">
    <form> 
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
          <select id="category" class="form-control" v-model.number="product.categoryId">
            <option value="1">Category1</option>
            <option value="2">Category2</option>
            <option value="3">Category3</option>
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
            <input class="custom-control-input" type="radio" id="isPerishableYes" name="isPerishable" value="yes" @click="product.isPerishable = true" />
            <label class="custom-control-label" for="isPerishableYes">Yes</label>
          </div>
          <div class="custom-control custom-radio col-md-2">
            <input class="custom-control-input" type="radio" id="isPerishableNo" name="isPerishable" value="no" @click="product.isPerishable = false" />
            <label class="custom-control-label" for="isPerishableNo">No</label>
          </div>
        </div>
      </fieldset>
      <div v-show="product.isPerishable">
        <fieldset class="form-group">
          <div class="row align-items-center justify-content-center">
            <legend class="col-md-2 col-form-label left-align">Keep in dry storage?</legend>
            <div class="custom-control custom-radio col-md-2">
              <input class="custom-control-input" type="radio" id="isRoomTempYes" name="isRoomTemp" value="yes" @click="product.keepRoom = true" />
              <label class="custom-control-label" for="isRoomTempYes">Yes</label>
            </div>
            <div class="custom-control custom-radio col-md-2">
              <input class="custom-control-input" type="radio" id="isRoomTempNo" name="isRoomTemp" value="no" @click="product.keepRoom = false" />
              <label class="custom-control-label" for="isRoomTempNo">No</label>
            </div>
          </div>
        </fieldset>
        <div v-show="product.keepRoom">
          <div class="form-group row align-items-center justify-content-center">
            <label for="name" class="col-md-2 col-form-label left-align">Dry Storage Shelf Life:</label>
            <div class="col-md-4">
              <input id="name" class="form-control" v-model.trim="product.shelfLifeRoom" />
            </div>
          </div>
          <div v-if="errors.keepRoomError" class="col-md-6 centered alert alert-danger">{{ errors.keepRoomErrorMsg }}</div>
        </div>
        <fieldset class="form-group">
          <div class="row align-items-center justify-content-center">
            <legend class="col-md-2 col-form-label left-align">Keep in cooler?</legend>
            <div class="custom-control custom-radio col-md-2">
              <input class="custom-control-input" type="radio" id="isFridgeYes" name="isFridge" value="yes" @click="product.keepFridge = true" />
              <label class="custom-control-label" for="isFridgeYes">Yes</label>
            </div>
            <div class="custom-control custom-radio col-md-2">
              <input class="custom-control-input" type="radio" id="isFridgeNo" name="isFridge" value="no" @click="product.keepFridge = false" />
              <label class="custom-control-label" for="isFridgeNo">No</label>
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
            <legend class="col-md-2 col-form-label left-align">Keep in freezer?</legend>
            <div class="custom-control custom-radio col-md-2">
              <input class="custom-control-input" type="radio" id="isFreezerYes" name="isFreezer" value="yes" @click="product.keepFreezer = true" />
              <label class="custom-control-label" for="isFreezerYes">Yes</label>
            </div>
            <div class="custom-control custom-radio col-md-2">
              <input class="custom-control-input" type="radio" id="isFreezerNo" name="isFreezer" value="no" @click="product.keepFreezer = false" />
              <label class="custom-control-label" for="isFreezerNo">No</label>
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
      <div class="center-align">
        <button class="btn btn-primary" type="button" @click="validateForm()">Submit</button>
      </div>
    </form>
    </div>
</template>

<script>
export default {
  name: 'ProductForm',
  props: {
    productToEdit: {
      type: Object,
      required: false,
    }
  },
  computed: {
    //not sure if this is actually necessary
    product() {
      const newProduct = this.productToEdit
      return newProduct
    }
  },
  methods: {
    validateForm() {
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
      }
      if(!this.checkErrors()) {
        console.dir(this.product)
        this.$emit('formValid', this.product)
        return true
      }

      //if we make it here there must be an error
      return false
    },
    checkErrors() {
      if(this.errors.nameError ||
      this.descriptionError ||
      this.categoryError ||
      this.priceError ||
      this.keepRoomError ||
      this.keepFridgeError ||
      this.keepFreezerError) {
        return true
      }

      return false
    }
  },
  data() {
    return {
      // product: {
      //   name:'',
      //   description: '',
      //   categoryId: 0,
      //   quantity: 0,
      //   price: 0,
      //   isPerishable: false,
      //   keepRoom: false,
      //   shelfLifeRoom: '',
      //   keepFridge: false,
      //   shelfLifeFridge: '',
      //   keepFreezer: false,
      //   shelfLifeFreezer: '',
      //   image: '',
      // },
      isPerishableYorN: '',
      isRoomTempYorN: '',
      isFridgeYorN: '',
      isFreezerYorN: '',
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
      },
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
