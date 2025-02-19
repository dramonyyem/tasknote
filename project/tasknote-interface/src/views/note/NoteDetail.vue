<template>
    <v-card
      elevation="16"
      class="mx-auto"
      width="600"
      min-height="500"
    >
      <v-card-title>{{ responseData.title }}</v-card-title>
      <v-card-subtitle>
        Created At: {{ formatDateTime(responseData.createdAt) }}
      </v-card-subtitle>
      <v-card-subtitle>
        Updated At: {{ formatDateTime(responseData.updatedAt) }}
      </v-card-subtitle>
  
      <v-card-text>
        {{ responseData.content || 'Loading content...' }}
      </v-card-text>
  
      <v-card-actions style="padding-top: 300px;">
        <v-btn color="red" @click="deleteNote">Delete</v-btn>
        <v-btn color="blue" @click="NoteEdit">Edit</v-btn>

      </v-card-actions>
    </v-card>
  </template>
  
  <script lang="ts">
  import { defineComponent, ref } from 'vue'
  import axios from 'axios';
  import { useRoute, useRouter } from 'vue-router'
  
  interface Note {
      noteId: number;
      title: string;
      content: string;
      createdAt: string;
      updatedAt: string;
  }
  
  // Date formatting function (same as before)
  
  
  export default defineComponent({
    name: 'NoteDetail',
    setup() {
      const route = useRoute();
      const router = useRouter();
      let noteId = route.params.id;
      const responseData = ref<Note[]>([]);  
      // Fetch the note data
      const getNote = async () => {
        try {
          const response = await axios.get(`http://localhost:5001/api/notes/${noteId}`, {
            headers: {
              'Authorization' : 'Bearer testing-token',
              'Accept': 'application/json, text/plain, */*',
            },
          });
          responseData.value = response.data;
        } catch (error) {
          console.error('Error fetching note:', error);
        }
      };


      const formatDateTime = (str: string) => {
      const date = new Date(str);
      return (
        date.getFullYear() +
        '-' + String(date.getMonth() + 1).padStart(2, '0') +
        '-' + String(date.getDate()).padStart(2, '0') +
        ' ' + String(date.getHours()).padStart(2, '0') +
        ':' + String(date.getMinutes()).padStart(2, '0') +
        ':' + String(date.getSeconds()).padStart(2, '0') +
        '.' + String(date.getMilliseconds()).padStart(3, '0')
      );
    };
  
      // Delete the note
      const deleteNote = async () => {
        try {
          await axios.delete(`http://localhost:5001/api/notes/${noteId}`, {
            headers: {
              'Authorization' : 'Bearer testing-token',
              'Accept': 'application/json, text/plain, */*',
            },
          });
          // Navigate to the NoteList page after successful deletion
          router.push({ name: 'NoteList' });
        } catch (error) {
          console.error('Error deleting note:', error);
        }
      };

      const NoteEdit = () => {
        router.push({ name: 'NoteEdit' ,params: { noteId }});
      }
  
     
      // Fetch the note data when the component is mounted
      getNote();
  
      return {
        responseData,
        deleteNote,
        formatDateTime,
        NoteEdit
      };
    },
  });
  </script>
  
  <style scoped>
  /* Your custom styles */
  </style>
  