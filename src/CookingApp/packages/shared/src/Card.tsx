import * as React from 'react'

interface CardProps {
    image: string
    description: string
    price: number
    heading: string
    onClick: () => void
    children: React.ReactNode
}

export const Card: React.FC<CardProps> = props => {
    return (
        <div className='item-wrapper'>
            <img src={props.image} alt={props.description} />
            <h3>{props.heading}</h3>
            <span>{props.price}</span>
            <button onClick={() => props.onClick()}>{props.children || 'Add to cart'}</button>
        </div>
    )
}

export default Card
