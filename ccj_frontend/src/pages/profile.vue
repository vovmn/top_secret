<template>
  <DefaultLayout>
    <BaseContainer>
      <v-row>
        <BaseColumn cols="12">
          <h1 class="font-weight-bold mb-4">Профиль</h1>
        </BaseColumn>
        <BaseColumn cols="6">
          <v-card title="О себе">
            <v-container>
              <v-row>
                <v-card-text class="text-grey text-uppercase">
                  <BaseColumn v-for="(value, key) in profileData" :key="key" cols="3">
                    {{ value }}
                  </BaseColumn>
                  <BaseColumn v-for="(value, key) in profileData" :key="key" cols="3">
                    {{ value }}
                  </BaseColumn>
                </v-card-text>
              </v-row>
            </v-container>
          </v-card>
        </BaseColumn>
      </v-row>
    </BaseContainer>
  </DefaultLayout>
</template>

<script setup lang="ts">
  import DefaultLayout from '@/layouts/DefaultLayout.vue'

  const profileData = [
    'Имя',
    'Фамилия',
    'Отчество',
  ]
</script>

<style scoped lang="sass">

</style>
