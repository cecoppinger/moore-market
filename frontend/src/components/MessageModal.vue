<template>
    <div>
        <!-- Button trigger modal -->
        <button type="button" class="btn btn-success"  @click="showModal = true">
        Add Feed Message
        </button>

        <!-- Modal -->
        <div v-if="showModal">
        <transition name="modal">
            <div class="modal-mask">

                <div class="modal-wrapper">
                    <div class="modal-dialog modal-dialog-centered" role="document">
                        <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Add Message</h5>
                            <button type="button" class="close" @click="showModal = false" aria-label="Close">
                            <span aria-hidden="true" >&times;</span>
                            </button>
                        </div>
                        <form @submit.prevent="validateForm()">
                            <div class="modal-body">
                                <div class="form-group">
                                    <label for="title">Message Title</label>
                                    <input type="text" class="form-control" v-model="message.title" id="title" name="title" aria-describedby="titleHelp" placeholder="Enter a title" required>
                                    <small id="titleHelp" class="form-text text-muted">Titles won't be displayed to the user, but will appear in your dashboard.</small>
                                </div>
                                <div class="form-group">
                                    <label for="messageBody">Message Body</label>
                                    <textarea class="form-control" v-model="message.body" id="messageBody" name="body" aria-describedby="bodyHelp" placeholder="Enter the message body" rows="2" required></textarea>
                                    <small id="bodyHelp" class="form-text text-muted">Message body won't appear in your dashboard, but will be displayed to the user.</small>
                                
                                </div>
                                <div class="form-group form-inline">
                                    <label for="startDate" class="mr-1">Start Date</label>
                                    <input type="date" class="form-control mb-1 mr-sm-2" id="startDate" name="startDate" v-model="message.startDate" required>
                                    <label for="endDate" class="mr-1">End Date</label>
                                    <input type="date" class="form-control mb-2 mr-sm-2" id="endDate" name="endDate" v-model="message.endDate" required :class="{ 'is-invalid': hasError }" >
                                    <div class="invalid-feedback">End Date cannot be before Start Date</div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" @click="showModal = false">Cancel</button>
                                <button type="submit" class="btn btn-primary">Submit</button>
                            </div>
                        </form>
                        </div>
                    </div>
                </div>
            </div>
        </transition>
        </div>
    </div>
</template>

<script>
import { mapActions, mapState } from 'vuex'
export default {
    name: 'MessageModal',
    data() {
       return {
         message: {
           title: '',
           body: '',
           startDate: '',
           endDate: '',
           userId: 0
         },
         showModal: false,  
         attemptSubmit: false,
         hasError: false,
       }
    },
    computed: {
        ...mapState('user', ['currentUser']),
        endBeforeStart() { return (this.message.endDate < this.message.startDate) }
    },
    methods: {
        ...mapActions('vendor', ['addNewMessage']),
        async validateForm() {
            this.attemptSubmit = true;
            if (this.endBeforeStart) {
                this.hasError = true
            } else {
                await this.addNewMessage(this.message);
                this.showModal = false
                this.attemptSubmit = false
                this.message.title = ''
                this.message.body = ''
                this.message.startDate = ''
                this.message.endDate = ''
                this.hasError = false
            }
        },
    },    
    mounted() {
        this.message.userId = this.currentUser.id;
    }
       
       
   } 
</script>

<style scoped>
.modal-mask {
    position: fixed;
    z-index: 9998;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0,0,0,.5);
    display: table;
    transition: opacity .3s ease;
}
.modal-wrapper {
    display: table-cell;
    vertical-align: middle;
}
</style>
