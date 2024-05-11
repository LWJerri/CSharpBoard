/**
 * This file was auto-generated by openapi-typescript.
 * Do not make direct changes to the file.
 */

export interface paths {
  "/lists": {
    get: operations["Get lists"];
    post: operations["Create list"];
  };
  "/lists/{id}": {
    get: operations["Get list"];
    delete: operations["Delete list"];
    patch: operations["Edit list"];
  };
  "/lists/{listId}/tasks": {
    get: operations["Get tasks"];
    post: operations["Create task"];
  };
  "/lists/{listId}/tasks/{id}": {
    get: operations["Get task"];
    delete: operations["Delete task"];
    patch: operations["Edit task"];
  };
}

export type webhooks = Record<string, never>;

export interface components {
  schemas: {
    CreateListDto: {
      name: string;
    };
    CreateTaskDto: {
      title: string;
      description: string;
      /** Format: date-time */
      dueAt: string;
      priority: components["schemas"]["PriorityEnum"];
    };
    PatchListDto: {
      name: string;
    };
    PatchTaskDto: {
      title?: string | null;
      description?: string | null;
      /** Format: date-time */
      dueAt?: string | null;
      priority?: components["schemas"]["PriorityEnum"];
      listId?: string | null;
    };
    /**
     * Format: int32
     * @enum {integer}
     */
    PriorityEnum: 0 | 1 | 2 | 3;
    ProblemDetails: {
      type?: string | null;
      title?: string | null;
      /** Format: int32 */
      status?: number | null;
      detail?: string | null;
      instance?: string | null;
      [key: string]: unknown;
    };
    ResponseListDto: {
      id: string;
      name: string;
      /** Format: date-time */
      createdAt: string;
      /** Format: date-time */
      updatedAt: string;
      /** Format: int32 */
      task: number;
    };
    ResponseTaskDto: {
      id: string;
      title: string;
      description: string;
      /** Format: date-time */
      dueAt: string;
      priority: components["schemas"]["PriorityEnum"];
      /** Format: date-time */
      createdAt: string;
      /** Format: date-time */
      updatedAt: string;
      listId: string;
    };
  };
  responses: never;
  parameters: never;
  requestBodies: never;
  headers: never;
  pathItems: never;
}

export type $defs = Record<string, never>;

export type external = Record<string, never>;

export interface operations {
  "Get lists": {
    responses: {
      /** @description Success */
      200: {
        content: {
          "application/json": components["schemas"]["ResponseListDto"][];
        };
      };
      /** @description Bad Request */
      400: {
        content: {
          "application/json": components["schemas"]["ProblemDetails"];
        };
      };
      /** @description Server Error */
      500: {
        content: never;
      };
    };
  };
  "Create list": {
    requestBody?: {
      content: {
        "application/json": components["schemas"]["CreateListDto"];
      };
    };
    responses: {
      /** @description Created */
      201: {
        content: {
          "application/json": components["schemas"]["ResponseListDto"];
        };
      };
      /** @description Bad Request */
      400: {
        content: {
          "application/json": components["schemas"]["ProblemDetails"];
        };
      };
      /** @description Server Error */
      500: {
        content: never;
      };
    };
  };
  "Get list": {
    parameters: {
      path: {
        id: string;
      };
    };
    responses: {
      /** @description Success */
      200: {
        content: {
          "application/json": components["schemas"]["ResponseListDto"];
        };
      };
      /** @description Not Found */
      404: {
        content: {
          "application/json": components["schemas"]["ProblemDetails"];
        };
      };
      /** @description Server Error */
      500: {
        content: never;
      };
    };
  };
  "Delete list": {
    parameters: {
      path: {
        id: string;
      };
    };
    responses: {
      /** @description No Content */
      204: {
        content: never;
      };
      /** @description Bad Request */
      400: {
        content: {
          "application/json": components["schemas"]["ProblemDetails"];
        };
      };
      /** @description Not Found */
      404: {
        content: {
          "application/json": components["schemas"]["ProblemDetails"];
        };
      };
      /** @description Server Error */
      500: {
        content: never;
      };
    };
  };
  "Edit list": {
    parameters: {
      path: {
        id: string;
      };
    };
    requestBody?: {
      content: {
        "application/json": components["schemas"]["PatchListDto"];
      };
    };
    responses: {
      /** @description Success */
      200: {
        content: {
          "application/json": components["schemas"]["ResponseListDto"];
        };
      };
      /** @description Bad Request */
      400: {
        content: {
          "application/json": components["schemas"]["ProblemDetails"];
        };
      };
      /** @description Not Found */
      404: {
        content: {
          "application/json": components["schemas"]["ProblemDetails"];
        };
      };
      /** @description Server Error */
      500: {
        content: never;
      };
    };
  };
  "Get tasks": {
    parameters: {
      path: {
        listId: string;
      };
    };
    responses: {
      /** @description Success */
      200: {
        content: {
          "application/json": components["schemas"]["ResponseTaskDto"][];
        };
      };
      /** @description Not Found */
      404: {
        content: {
          "application/json": components["schemas"]["ProblemDetails"];
        };
      };
      /** @description Server Error */
      500: {
        content: never;
      };
    };
  };
  "Create task": {
    parameters: {
      path: {
        listId: string;
      };
    };
    requestBody?: {
      content: {
        "application/json": components["schemas"]["CreateTaskDto"];
      };
    };
    responses: {
      /** @description Created */
      201: {
        content: {
          "application/json": components["schemas"]["ResponseTaskDto"];
        };
      };
      /** @description Bad Request */
      400: {
        content: {
          "application/json": components["schemas"]["ProblemDetails"];
        };
      };
      /** @description Server Error */
      500: {
        content: never;
      };
    };
  };
  "Get task": {
    parameters: {
      path: {
        listId: string;
        id: string;
      };
    };
    responses: {
      /** @description Success */
      200: {
        content: {
          "application/json": components["schemas"]["ResponseTaskDto"];
        };
      };
      /** @description Not Found */
      404: {
        content: {
          "application/json": components["schemas"]["ProblemDetails"];
        };
      };
      /** @description Server Error */
      500: {
        content: never;
      };
    };
  };
  "Delete task": {
    parameters: {
      path: {
        listId: string;
        id: string;
      };
    };
    responses: {
      /** @description No Content */
      204: {
        content: never;
      };
      /** @description Bad Request */
      400: {
        content: {
          "application/json": components["schemas"]["ProblemDetails"];
        };
      };
      /** @description Server Error */
      500: {
        content: never;
      };
    };
  };
  "Edit task": {
    parameters: {
      path: {
        listId: string;
        id: string;
      };
    };
    requestBody?: {
      content: {
        "application/json": components["schemas"]["PatchTaskDto"];
      };
    };
    responses: {
      /** @description Success */
      200: {
        content: {
          "application/json": components["schemas"]["ResponseTaskDto"];
        };
      };
      /** @description Bad Request */
      400: {
        content: {
          "application/json": components["schemas"]["ProblemDetails"];
        };
      };
      /** @description Not Found */
      404: {
        content: {
          "application/json": components["schemas"]["ProblemDetails"];
        };
      };
      /** @description Server Error */
      500: {
        content: never;
      };
    };
  };
}
