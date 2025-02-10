<template>
    <v-card                     
        elevation="16"
        class="mx-auto"
        width="400"
        min-height="500"
        title="Note"
        subtitle="Edit"
        >
        <v-form @submit.prevent="updateNote" class="mx-4 mt-4">
          <v-text-field
            label="Title"
            v-model="dataInput.title"
          ></v-text-field>
  
          <v-text-field
            label="Content"
            v-model="dataInput.content"
  
          ></v-text-field>
  
          <v-select
              label="Select User"
              v-model="dataInput.userId"
              :items="selectUser"
              item-title="name"
              item-value="userId"
          ></v-select>
          <v-btn class="mt-2" type="submit" block>Update</v-btn>
        </v-form>
    </v-card>
</template>

<script lang="ts">
import axios from 'axios';
import { defineComponent, ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'


export default defineComponent({
    name: 'NoteEdit',
    setup () {
        
        const dataInput = ref({
            title : '',
            content: '',
            userId: null,
        });

        const responseData = ref([]);
        const route = useRoute();
        const router = useRouter();

        let noteId = route.params.id;
        const selectUser = ref([]);
        const getNote = async () => {
        try {
            const response = await axios.get(`http://localhost:5001/api/notes/${noteId}`, {
                headers: {
                'Accept': 'application/json, text/plain, */*',
                },
            });
            console.log(response);
                if (response.data) {
                    dataInput.value.userId = response.data.userId || null;
                    dataInput.value.title = response.data.title || '';
                    dataInput.value.content = response.data.content || '';
                } else {
                    console.error('Note data is missing');
                }
            } catch (error) {
                console.error('Error fetching note:', error);
            }
        };
  
        const updateNote = async () => {
            try {
                await axios.put(`http://localhost:5001/api/notes/${noteId}`, 
                    { 
                        title: dataInput.value.title,
                        content: dataInput.value.content,
                        userId: dataInput.value.userId,
                    }, { 
                    headers: {
                        'Authorization' : 'Bearer testing-token',
                        'Accept': 'application/json, text/plain, */*',
                    },
                });
                router.push({ name: 'NoteList' });
            } catch (error) {
                
            }
        };


        const userData = async () => {
            try {
                const response = await axios.get("http://localhost:5001/api/users", {
                    headers: {
                        'Authorization' : 'Bearer testing',
                        'Accept': 'application/json, text/plain, */*',
                    },
                });
                selectUser.value = response.data

            } catch (error) {

            }
        };

        userData();
        getNote();
        return {
            dataInput,
            selectUser,
            updateNote,
        }
    }
})
</script>

<style scoped>

</style>