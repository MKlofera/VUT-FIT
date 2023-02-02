/*
* Binárny vyhľadávací strom — iteratívna varianta
*
* S využitím dátových typov zo súboru btree.h, zásobníkov zo súborov stack.h a
* stack.c a pripravených kostier funkcií implementujte binárny vyhľadávací
* strom bez použitia rekurzie.
*/

#include "../btree.h"
#include "stack.h"
#include <stdio.h>
#include <stdlib.h>

/*
 * Inicializácia stromu.
 *
 * Užívateľ musí zaistiť, že incializácia sa nebude opakovane volať nad
 * inicializovaným stromom. V opačnom prípade môže dôjsť k úniku pamäte (memory
 * leak). Keďže neinicializovaný ukazovateľ má nedefinovanú hodnotu, nie je
 * možné toto detegovať vo funkcii.
 */
void bst_init(bst_node_t **tree) {
    if (!tree || !(*tree)) {
        return;
    }
    *tree = NULL;
}

/*
 * Nájdenie uzlu v strome.
 *
 * V prípade úspechu vráti funkcia hodnotu true a do premennej value zapíše
 * hodnotu daného uzlu. V opačnom prípade funckia vráti hodnotu false a premenná
 * value ostáva nezmenená.
 *
 * Funkciu implementujte iteratívne bez použitia vlastných pomocných funkcií.
 */
bool bst_search(bst_node_t *tree, char key, int *value) {
    bool loop = true;
    bool found = false;

    // search
    if (tree == NULL) {
        loop = NULL;
    } else {
        while (loop && tree != NULL) {
            if (key < tree->key) {
//                go left
                tree = tree->left;
            } else if (key > tree->key) {
//                go right
                tree = tree->right;
            } else {
//                found
                found = true;
                loop = false;
                *value = tree->value;
            }
        }
        return found;
    }
    return found;
}

/*
 * Vloženie uzlu do stromu.
 *
 * Pokiaľ uzol so zadaným kľúčom v strome už existuje, nahraďte jeho hodnotu.
 * Inak vložte nový listový uzol.
 *
 * Výsledný strom musí spĺňať podmienku vyhľadávacieho stromu — ľavý podstrom
 * uzlu obsahuje iba menšie kľúče, pravý väčšie.
 *
 * Funkciu implementujte iteratívne bez použitia vlastných pomocných funkcií.
 */
void bst_insert(bst_node_t **tree, char key, int value) {
    bool found = false;
    bool loop = true;
    struct bst_node *where = malloc(sizeof(struct bst_node));
    if (where == NULL) {
        fprintf(stderr, "MALLOC ERROR");
        
        return;
    }
    where = NULL;

    // TODO OPTIMALIZATION -- > DELETE SEARCH AND CALL FUNCTION SEARCH INSTEAD!!
    // search
    if (*tree == NULL) {
        loop = NULL;
    } else {
        while (loop && *tree != NULL) {
            where = *tree; //  keep where did we found the key
            if (key < (*tree)->key) {
//                go left
                tree = &(*tree)->left;
            } else if (key > (*tree)->key) {
//                go right
                tree = &(*tree)->right;
            } else {
//                found
                found = true;
                loop = false;
            }
        }
    }
//    insert
    if (found) {
        where->value = value;
    } else {
        struct bst_node *newList = malloc(sizeof(struct bst_node));
        if (!newList) {
            fprintf(stderr, "MALLOC ERROR");
            return;
        }
        newList->value = value;
        newList->key = key;
        newList->left = newList->right = NULL;
        if (where == NULL) {
            *tree = newList; // new root to empty tree
        } else {
            if (key < where->key) {
                where->left = newList;
            } else {
                where->right = newList;
            }
        }
    }

}

/*
 * Pomocná funkcia ktorá nahradí uzol najpravejším potomkom.
 *
 * Kľúč a hodnota uzlu target budú nahradené kľúčom a hodnotou najpravejšieho
 * uzlu podstromu tree. Najpravejší potomok bude odstránený. Funkcia korektne
 * uvoľní všetky alokované zdroje odstráneného uzlu.
 *
 * Funkcia predpokladá že hodnota tree nie je NULL.
 *
 * Táto pomocná funkcia bude využitá pri implementácii funkcie bst_delete.
 *
 * Funkciu implementujte iteratívne bez použitia vlastných pomocných funkcií.
 */
void bst_replace_by_rightmost(bst_node_t *target, bst_node_t **tree) {
    while (1) {
        if (target == NULL || tree == NULL) {
            return;
        }
            // get rightmost node
        else if ((*tree)->right != NULL) {
            tree = &(*tree)->right;
        }
            // found rightmost node.
        else {

            char key = (*tree)->key;
            int value = (*tree)->value;

            bst_delete(tree, (*tree)->key);
            //rewrite the value and key
            target->key = key;
            target->value = value;


            break;
        }
    }
}

/*
 * Odstránenie uzlu v strome.
 *
 * Pokiaľ uzol so zadaným kľúčom neexistuje, funkcia nič nerobí.
 * Pokiaľ má odstránený uzol jeden podstrom, zdedí ho otec odstráneného uzla.
 * Pokiaľ má odstránený uzol oba podstromy, je nahradený najpravejším uzlom
 * ľavého podstromu. Najpravejší uzol nemusí byť listom!
 * Funkcia korektne uvoľní všetky alokované zdroje odstráneného uzlu.
 *
 * Funkciu implementujte iteratívne pomocou bst_replace_by_rightmost a bez
 * použitia vlastných pomocných funkcií.
 */
void bst_delete(bst_node_t **tree, char key) {
    if (tree == NULL || *tree == NULL) {
        return;
    } else {
        while (*tree != NULL) {
            //deleting key is on left side of tree -> we go left
            if (key < (*tree)->key) {
                tree = &(*tree)->left;
            }
                // deleting key is on right side of tree -> we go right
            else if (key > (*tree)->key) {
                tree = &(*tree)->right;
            } else {// found deleting node, how many sons does node have?

                //node has no sons
                if ((*tree)->left == NULL && (*tree)->right == NULL) {
                    free((*tree));
                    *tree = NULL;
                }
                    // node has two sons
                else if ((*tree)->left != NULL && (*tree)->right != NULL) {
                    bst_replace_by_rightmost(*tree, &(*tree)->left);
                }
                    // node has only one child
                else {
                    struct bst_node *tmp;

                    // node has left child
                    if ((*tree)->left) {
                        tmp = (*tree)->left;
                    }
                        // node has right child
                    else {
                        tmp = (*tree)->right;
                    }
                    free((*tree));
                    *tree = tmp;
                }
                break;
            }
        }
    }
}

/*
 * Zrušenie celého stromu.
 *
 * Po zrušení sa celý strom bude nachádzať v rovnakom stave ako po
 * inicializácii. Funkcia korektne uvoľní všetky alokované zdroje rušených
 * uzlov.
 *
 * Funkciu implementujte iteratívne pomocou zásobníku uzlov a bez použitia
 * vlastných pomocných funkcií.
 */
void bst_dispose(bst_node_t **tree) {
    if (tree == NULL || *tree == NULL) {
        return;
    }
    stack_bst_t stack;
    stack_bst_init(&stack);

    stack_bst_push(&stack, *tree);
    while (!stack_bst_empty(&stack)) {

        bst_node_t *tmp = stack_bst_top(&stack);
        stack_bst_pop(&stack);

        if (tmp->right) {
            stack_bst_push(&stack, tmp->right);
        }
        if (tmp->left) {
            stack_bst_push(&stack, tmp->left);
        }

        free(tmp);
    }
    *tree = NULL;
}

/*
 * Pomocná funkcia pre iteratívny preorder.
 *
 * Prechádza po ľavej vetve k najľavejšiemu uzlu podstromu.
 * Nad spracovanými uzlami zavola bst_print_node a uloží ich do zásobníku uzlov.
 *
 * Funkciu implementujte iteratívne pomocou zásobníku uzlov a bez použitia
 * vlastných pomocných funkcií.
 */
void bst_leftmost_preorder(bst_node_t *tree, stack_bst_t *to_visit) {
    while (tree != NULL) {
        bst_print_node(tree);
        if (tree->left) {
            stack_bst_push(to_visit, tree);
        }
        tree = tree->left;
    }
}

/*
 * Preorder prechod stromom.
 *
 * Pre aktuálne spracovávaný uzol nad ním zavolajte funkciu bst_print_node.
 *
 * Funkciu implementujte iteratívne pomocou funkcie bst_leftmost_preorder a
 * zásobníku uzlov bez použitia vlastných pomocných funkcií.
 */
void bst_preorder(bst_node_t *tree) {
    if (tree == NULL) {
        return;
    }
    stack_bst_t stack;
    stack_bst_init(&stack);

    bst_leftmost_preorder(tree, &stack);
    while (!stack_bst_empty(&stack)) {

        bst_node_t *tmp = stack_bst_top(&stack);
        bst_leftmost_preorder(tmp->right, &stack);
        stack_bst_pop(&stack);
    }

}

/*
 * Pomocná funkcia pre iteratívny inorder.
 *
 * Prechádza po ľavej vetve k najľavejšiemu uzlu podstromu a ukladá uzly do
 * zásobníku uzlov.
 *
 * Funkciu implementujte iteratívne pomocou zásobníku uzlov a bez použitia
 * vlastných pomocných funkcií.
 */
void bst_leftmost_inorder(bst_node_t *tree, stack_bst_t *to_visit) {
    while (tree != NULL) {
        stack_bst_push(to_visit, tree);
        tree = tree->left;
    }
}

/*
 * Inorder prechod stromom.
 *
 * Pre aktuálne spracovávaný uzol nad ním zavolajte funkciu bst_print_node.
 *
 * Funkciu implementujte iteratívne pomocou funkcie bst_leftmost_inorder a
 * zásobníku uzlov bez použitia vlastných pomocných funkcií.
 */
void bst_inorder(bst_node_t *tree) {
    if (tree == NULL) {
        return;
    }
    stack_bst_t stack;
    stack_bst_init(&stack);

    bst_leftmost_inorder(tree, &stack);

    while (!stack_bst_empty(&stack)) {
        bst_node_t *tmp = stack_bst_top(&stack);
        stack_bst_pop(&stack);
        bst_print_node(tmp);
        bst_leftmost_inorder(tmp->right, &stack);
    }
}

/*
 * Pomocná funkcia pre iteratívny postorder.
 *
 * Prechádza po ľavej vetve k najľavejšiemu uzlu podstromu a ukladá uzly do
 * zásobníku uzlov. Do zásobníku bool hodnôt ukladá informáciu že uzol
 * bol navštívený prvý krát.
 *
 * Funkciu implementujte iteratívne pomocou zásobníkov uzlov a bool hodnôt a bez použitia
 * vlastných pomocných funkcií.
 */
void bst_leftmost_postorder(bst_node_t *tree, stack_bst_t *to_visit,
                            stack_bool_t *first_visit) {

    while (tree != NULL) {
        stack_bst_push(to_visit, tree);
        stack_bool_push(first_visit, true);
        tree = tree->left;
    }
}

/*
 * Postorder prechod stromom.
 *
 * Pre aktuálne spracovávaný uzol nad ním zavolajte funkciu bst_print_node.
 *
 * Funkciu implementujte iteratívne pomocou funkcie bst_leftmost_postorder a
 * zásobníkov uzlov a bool hodnôt bez použitia vlastných pomocných funkcií.
 */
void bst_postorder(bst_node_t *tree) {
    if (tree == NULL) {
        return;
    }

    //stack with tree node key's
    stack_bst_t stack;
    stack_bst_init(&stack);

    //stack if tree node was visited once -> true
    stack_bool_t stack_first_visit;
    stack_bool_init(&stack_first_visit);

    // fill stack's with first run
    bst_leftmost_postorder(tree, &stack, &stack_first_visit);

    //while stack's are not empty
    while (!stack_bst_empty(&stack)) {
        bst_node_t *tmp = stack_bst_top(&stack);

        //tree node was visited only once
        if (stack_bool_top(&stack_first_visit) == true) {

            // change state visited once to visited twice -> true to false
            stack_bool_pop(&stack_first_visit);
            stack_bool_push(&stack_first_visit, false);

            // tree node has no right son
            if (tmp->right == NULL) {
                // print node and delete from stack's
                bst_print_node(tmp);
                stack_bool_pop(&stack_first_visit);
                stack_bst_pop(&stack);
            } else {
                // go right
                bst_leftmost_postorder(tmp->right, &stack, &stack_first_visit);
            }

        }
            // tree node was visited twice
        else {
            // print tree node and delete from stack's
            bst_print_node(tmp);
            stack_bst_pop(&stack);
            stack_bool_pop(&stack_first_visit);
        }
    }
}
