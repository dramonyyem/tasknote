<template>
    <div class="mx-auto" style="width: 600px;">
        <v-btn to="/note/create" block>Add Note</v-btn>
    </div>
    <div class="mx-auto" style="width: 100%;">
        
        <v-card
            class="mt-2 px-2 pt-2"
        >
            <v-text-field
                label="Search Title"
                v-model="search"
            >
            

            </v-text-field>
            <v-btn class="mx-2 my-2" @click="sortbyDesc">
                Sort By DESC 
            </v-btn>
            <v-btn class="mx-2 my-2" @click="sortbyAsc">
                Sort By ASC 
            </v-btn>
        </v-card>
        <v-row
            class="mx-auto my-8"
            >
            <v-col
                v-for="item in searchData"
                cols="6"
            >
                <v-card
                    @mouseover="hover = item.noteId"
                    @mouseleave="hover = null"
                    @click="messageData(item.noteId)"
                    :color="hover === item.noteId ? 'red-lighten-2' : '' "
                    elevation="16"
                    min-height="200px"
                    class="hover-box mx-auto my-8 border-radius-4"
                    >
                    <v-card-item>
                    <v-card-title>{{ item.title }}</v-card-title>

                </v-card-item>
                    <v-card-subtitle>Created At: {{ formatDateTime(item.createdAt) }}, Updated At: {{ formatDateTime(item.updatedAt) }}</v-card-subtitle>
                    <v-card-text>
                        {{ item.content }}
                    </v-card-text>

                    </v-card>
            </v-col>
        </v-row>
    </div>
    
</template>
<script lang="ts">
import axios from 'axios';
import { computed, defineComponent, ref } from 'vue'
import { useRouter } from 'vue-router';

interface Note {
    noteId: number;
    title: string;
    content: string;
    createdAt: string;
    updatedAt: string;
}

export default defineComponent({
    name: 'NoteList',
    setup () {
        const responseData = ref<Note[]>([]); // should create interface or class for assign specificate value
        const router = useRouter();
        const hover = ref<number | null>(null);
        const search = ref('');
        const fetchData = async () => {
            try {
                const response = await axios.get("http://localhost:5001/api/notes",{ 
                    headers: {
                        'Authorization' : 'Bearer testing-token',
                    }
                });
                responseData.value = response.data;
            } catch (error) {
                
            }
        }

        const checkId = (id : number) => {
            alert(id);
        }

        const searchData = computed(() => {
            if (search.value === '') {
                return responseData.value; // Return all data if search is empty
            }
            return responseData.value.filter((item) => {
                const title = item.title ? item.title.toLowerCase() : '';
                // Perform a case-insensitive search on both title and content
                return title.toLowerCase().includes(search.value.toLowerCase());
            });
        });


        const formatDateTime = (str : string) => {
            const date = new Date(str);
            const formatted = date.getFullYear() +
                '-' + String(date.getMonth() + 1).padStart(2, '0') +
                '-' + String(date.getDate()).padStart(2, '0') +
                ' ' + String(date.getHours()).padStart(2, '0') +
                ':' + String(date.getMinutes()).padStart(2, '0') +
                ':' + String(date.getSeconds()).padStart(2, '0'); // milliseconds with 3 digits
            return formatted;
        }

        const messageData = (id: number) => {
            router.push({ name: 'NoteDetail', params: { id } });

        }

        const sortbyDesc = () => {
            return searchData.value.sort((a, b) => b.noteId - a.noteId);
        }
        const sortbyAsc = () => {
            return searchData.value.sort((a, b) => a.noteId - b.noteId);
        }
        fetchData();
        return {
            fetchData,
            responseData,
            hover,
            formatDateTime,
            messageData,
            search,
            searchData,
            sortbyDesc,
            sortbyAsc
        }
    }
})
</script>

<style scoped>
.hover-box {
  padding: 20px;
  text-align: center;
  cursor: pointer;
  transition: background-color 0.3s ease;
}
</style>