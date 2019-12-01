<template>
  <div class="page-container">
    <md-app>
      <md-app-toolbar class="md-primary">
        <span class="md-title">My Classroom</span>
      </md-app-toolbar>

      <md-app-drawer md-permanent="full">
        <md-toolbar class="md-transparent" md-elevation="0">
          Workbooks
        </md-toolbar>

        <md-list>
          <md-list-item :to="'/editor/' + book.id" v-for="book in books" :key="book.id">
            <md-icon>library_books</md-icon>
            <span class="md-list-item-text">{{book.name}}</span>
          </md-list-item>
        </md-list>
        <md-button class="md-primary md-raised" @click="openDialog">Add</md-button>
      </md-app-drawer>

      <md-app-content>
        <router-view />
      </md-app-content>
    </md-app>

    <md-dialog-prompt :md-active.sync="showDialog"
                      md-title="Add new book"
                      md-confirm-text="Add"
                      v-model="newBook"
                      @md-confirm="addNewBook">
    </md-dialog-prompt>
  </div>
</template>

<script>
  import axios from 'axios'

  export default {
    data() {
      return {
        books: [],
        showDialog: false,
        newBook: ''
      }
    },
    async created() {
      const { data } = await axios.get('/api/books');
      this.books = data;
      if (this.books.length === 0) {
        this.openDialog();
      }
    },
    methods: {
      openDialog() {
        this.newBook = '';
        this.showDialog = true;
      },
      async addNewBook() {
        const { data } = await axios.post('/api/books', {name: this.newBook});
        this.books = data.books;
        await this.$router.push(`/editor/${data.id}`);
      }
    }
  }
</script>

<style lang="scss" scoped>
  .md-app {
    border: 1px solid rgba(#000, .12);
  }

  // Demo purposes only
  .md-drawer {
    width: 210px;
    max-width: calc(100vw - 125px);
  }
</style>
