<template>
    <v-card                     
        elevation="16"
        class="mx-auto"
        width="600"
        min-height="500"
        title="Note"
        subtitle="Create"
        >
        <v-form @submit.prevent="createNote" class="mx-4 mt-4">
          <v-text-field
            label="Title"
            v-model="dataInput.title"
          ></v-text-field>
  
          <v-text-field
            label="Content"
            v-model="dataInput.content"
  
          ></v-text-field>
  
          <!-- <v-select
              label="Select User"
              v-model="dataInput.userId"
              :items="selectUser"
              item-title="name"
              ></v-select> -->
          <v-btn class="mt-2" type="submit" block>Submit</v-btn>
        </v-form>
    </v-card>
</template>

<script lang="ts">
import axios from 'axios';
import { defineComponent, ref } from 'vue'
import { useRouter } from 'vue-router';

export default defineComponent({
    name: 'NoteCreate',
    setup () {
        const router = useRouter();
        const dataInput = ref({
            title: '',
            content: '',
            userId: '',
        });

        const selectUser = ref([]);
        const createNote = async () => {
            try {
                console.log(dataInput.value);
                await axios.post('http://localhost:5001/api/notes',  
                    { 
                        headers: {
                            'Authorization' : 'Bearer testing-token',
                            'Accept': 'application/json, text/plain, */*',
                            'Content-Type': 'application/json',
                        },
                        title: dataInput.value.title,
                        content: dataInput.value.content,
                        // userId: dataInput.value.userId
                    }   
                );

                router.push({ name: 'NoteList' });
                
            } catch (error) {
                
            }
        };


        const userData = async () => {
            try {
                const response = await axios.get("http://localhost:5001/api/users", {
                    headers: {
                        'Authorization' : 'Bearer valid-api-token',
                        'Accept': 'application/json, text/plain, */*',
                    },
                });
                selectUser.value = response.data
                console.log(response.data);
            } catch (error) {

            }
        };

        userData();
        return {
            dataInput,
            selectUser,
            createNote,
        }
    }
})
</script>

<style scoped>

</style>