<template>
  <div class='register-user container'>
    <div v-if="errorMsg">
      {{ errorMsg }}
    </div>      
      <form @submit.prevent="handleSubmit()">
        <div class="form-group row justify-content-center">
          <label class="col-form-label col-sm-2 left-align" for="username">Username</label>
          <div class="col-sm-6">
          <input class="form-control" id="username" v-model.trim="user.username" />
          </div>
        </div>
        <div class="form-group row justify-content-center">
          <label class="col-form-label col-sm-2 left-align" for="password">Password</label>
          <div class="col-sm-6">
          <input class="form-control" id="password" type="password" v-model="user.password" />
          </div>
        </div>
        <div class="form-group row justify-content-center">
          <label class="col-form-label col-sm-2 left-align" for="verify-pw">Verify Password</label>
          <div class="col-sm-6">
          <input class="form-control" type="password" id="verify-pw" v-model="verify" />
          </div>
        </div>
        <div class="form-group row justify-content-center">
          <label class="col-form-label col-sm-2 left-align" for="verify-pw">Vendor?</label>
          <div class="col-sm-6">
          <input class="form-control" type="checkbox" v-model="isVendor" />
          </div>
        </div>
        <div class="form-group row justify-content-center">
          <input type="submit" class="btn btn-primary" value="Register" />
        </div>
      </form>
  </div>
</template>

<script>
import { mapActions } from 'vuex'

export default {
  name: 'RegisterUser',
  data() {
    return {
      user: {
        username: '',
        password: '',  
        accountType: 1,      
      },
      isVendor: false,
      verify: '',
      errorMsg: '',
    };
  },
  methods: {
    ...mapActions('user', ['registerUser']),
    handleSubmit(e) {
      if(this.user.password !== this.verify) {
        this.errorMsg = 'Password and verify password fields must match';        
      } else if(this.user.username === '' || !this.user.username) {
        this.errorMsg = 'Username field is required'
      } else {
        //register new user
        if(isVendor) {
          this.user.accountType = 0
        }
        this.registerUser(this.user)
      }
    }
  },
}
</script>

<style scoped>
.left-align {
  text-align: left;
}
</style>
