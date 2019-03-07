<template>
    <div class='container-fluid'>
        <form v-on:submit.prevent="">
            <div class="form-group-sm">
                <input v-model='categoryName' @input='sendCategory()' type="text" class="form-control form-control-sm" id="CategoryName" placeholder="New Category Name">
            </div>
        </form>
    </div>
</template>

<script>

export default {
    name:'ProductCategoryForm',
    props: ['formType', 'oldCategoryName'],
    data() {
        return {
            categoryName: ''
        }
    },

    methods: {
        sendCategory(){
            this.$emit('category-input', this.categoryName)
        }
    },
    created() {
        this.categoryName = this.oldCategoryName
    }
}
</script>

<style>

</style>
