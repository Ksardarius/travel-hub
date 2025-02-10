import {createChannel, createClient} from 'nice-grpc'

import type {RecipesClient} from '../generated/recipes'
import {RecipesDefinition} from '../generated/recipes'

let _client: RecipesClient

function getClient(): RecipesClient {
    if (!_client) {
        const address = process.env.API_URL
        if (!address) {
            throw new Error('API URL is not set')
        }
        const channel = createChannel(address)

        _client = createClient(RecipesDefinition, channel)
    }
    return _client
}

export async function getAllRecipes() {
    return await getClient().getAllRecipes({
        pageIndex: 0,
        pageSize: 5
    })
}
