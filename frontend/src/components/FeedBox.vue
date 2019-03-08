<template>
    <div class="card border-success mx-2 mb-3">
      <div class="card-header">
          <h4 class="d-inline">Your Feed</h4>
          <MessageModal class="d-inline float-right"></MessageModal>
      </div>    
      <div class="card-body">
        <table class='table'>
            <thead>
                <tr>
                    <th v-for="columnTitle in columnTitles" :key='columnTitle.id'>{{ columnTitle }}</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="message in vendorMessages"  :key='message.id'>
                  <td>{{ message.startDate.slice(0,10)}}</td>  
                  <td>{{ message.endDate.slice(0,10) }}</td>
                  <td>{{ message.title }}</td>
                  <td>
                      <div class="btn-group btn-group-sm" role="group" aria-label="editremove">
                        <button type="button" class="btn btn-success" @click="initiateEdit(message.id)">Edit</button>
                        <button type="button" class="btn btn-danger" @click="initiateRemove(message.id)">Delete</button>
                      </div>
                  </td>
                </tr>    
            </tbody>
        </table>
        <div v-if="showRemoveModal">
            <transition name="modal">
            <div class="modal-mask">
                <div class="modal-wrapper">
                    <div class="modal-dialog modal-dialog-centered" role="document">
                        <div class="modal-content">
                            <div class="modal-body">
                                Are you sure?
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" @click="showRemoveModal = false">No</button>
                                <button type="submit" class="btn btn-primary" @click="confirmRemove(messageIdToRemove)">Yes</button>
                            </div>
                        </div>
                    </div>        
                </div>
            </div>
            </transition>    
        </div>
        <!-- Edit Modal -->
        <div v-if="showEditModal">
        <transition name="modal">
            <div class="modal-mask">

                <div class="modal-wrapper">
                    <div class="modal-dialog modal-dialog-centered" role="document">
                        <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Edit Message</h5>
                            <button type="button" class="close" @click="showEditModal = false" aria-label="Close">
                            <span aria-hidden="true" >&times;</span>
                            </button>
                        </div>
                        <form @submit.prevent="validateForm()">
                            <div class="modal-body">
                                <div class="form-group">
                                    <label for="title">Message Title</label>
                                    <input type="text" class="form-control" v-model="messageToEdit.title" id="title" name="title" aria-describedby="titleHelp" placeholder="Enter a title" required>
                                    <small id="titleHelp" class="form-text text-muted">Titles won't be displayed to the user, but will appear in your dashboard.</small>
                                </div>
                                <div class="form-group">
                                    <label for="messageBody">Message Body</label>
                                    <textarea class="form-control" v-model="messageToEdit.body" id="messageBody" name="body" aria-describedby="bodyHelp" placeholder="Enter the message body" rows="2" required></textarea>
                                    <small id="bodyHelp" class="form-text text-muted">Message body won't appear in your dashboard, but will be displayed to the user.</small>
                                
                                </div>
                                <div class="form-group form-inline">
                                    <label for="startDate" class="mr-1">Start Date</label>
                                    <input type="date" class="form-control mb-1 mr-sm-2" id="startDate" name="startDate" v-model="messageToEdit.startDate" required>
                                    <label for="endDate" class="mr-1">End Date</label>
                                    <input type="date" class="form-control mb-2 mr-sm-2" id="endDate" name="endDate" v-model="messageToEdit.endDate" required :class="{ 'is-invalid': hasError }" >
                                    <div class="invalid-feedback">End Date cannot be before Start Date</div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" @click="showEditModal = false">Cancel</button>
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
    </div>
</template>

<script>
import MessageModal from '../components/MessageModal.vue';
import { mapGetters, mapActions } from 'vuex';
export default {
    name: 'FeedBox',
    components: {
        MessageModal
    },
    props: ['vendorMessages'],
    data() {
        return {
            columnTitles: ["Start Date", "Ends On", "Message Title", "Edit/Delete"],
            messageIdToRemove: 0,
            messageIdToEdit: 0,
            showRemoveModal: false,
            showEditModal: false,
            messageToEdit: {
                title: '',
                body: '',
                startDate: '',
                endDate: '',
                UserId: 0,
            },
            attemptSubmit: false,
            hasError: false, 
        }
    },
    computed: {
        ...mapGetters('vendor', ['getMessageById']),
        endBeforeStart() {return (this.messageToEdit.endDate < this.messageToEdit.startDate) }
    },
    methods: {
        ...mapActions('vendor', ['editMessageById', 'removeMessageById']),
        initiateRemove(id) {
            this.showRemoveModal = true;
            this.messageIdToRemove = id;
        },
        confirmRemove(id) {
            this.showRemoveModal = false;
            this.removeMessageById(id);
        },
        initiateEdit(id) {
            this.messageToEdit = this.getMessageById(id);
            this.messageToEdit.startDate = this.messageToEdit.startDate.slice(0,10);
            this.messageToEdit.endDate = this.messageToEdit.endDate.slice(0,10);
            this.showEditModal = true;
        },
        validateForm() {
            this.attemptSubmit = true;
            if (this.endBeforeStart) {
                this.hasError = true
            } else {
                this.editMessageById(this.messageToEdit)
                this.showEditModal = false
                this.attemptSubmit = false
                this.hasError = false
            }
        }
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
    background-color: rgba(0, 0, 0, 0.5);
    display: table;
    transition: opacity .3s ease;
}
.modal-wrapper {
    display: table-cell;
    vertical-align: middle;
}
</style>